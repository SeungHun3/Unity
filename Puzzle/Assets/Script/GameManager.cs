using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("----------Core")] // 카테고리만들기
    public int score;
    public int maxLevel = 2;
    public bool isOver;

    [Header("----------Object Pooling")]
    public GameObject donglePrefab;
    public Transform dongleGroup;
    public List<Dongle> donglePool;

    public GameObject effectPrefab;
    public Transform effectGroup;
    public List<ParticleSystem> effectPool;

    [Range(1,30)]
    public int poolSize;
    public int poolCursor; // 오브젝트관리를 위한 변수
    
    public Dongle lastDongle;

    [Header("----------Audio")]
    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayer;
    int sfxCursor;
    public AudioClip[] sfxClip;
    public enum SfxType { LevelUp, Next, Attach, Button, Over};

    [Header("----------UI")]    
    public GameObject startGroup;
    public GameObject endGroup;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI subScoreText;

    [Header("ETC")]
    public GameObject etcGround;
    

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        donglePool = new List<Dongle>();
        effectPool = new List<ParticleSystem>();
        for(int index = 0; index < poolSize; index++)
        {
            MakeDongle();
        }

        if(!PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }
        maxScoreText.text = PlayerPrefs.GetInt("MaxScore").ToString();
    }
    public void GameStart()
    {
        // 오브젝트 세팅
        etcGround.SetActive(true);
        scoreText.gameObject.SetActive(true);
        maxScoreText.gameObject.SetActive(true);
        startGroup.SetActive(false);

        bgmPlayer.Play();
        SfxPlay(SfxType.Button);
        // 게임시작
        Invoke("NextDongle",1.5f);
    }
    Dongle MakeDongle()
    {
        
        GameObject instantEffectObj = Instantiate(effectPrefab, effectGroup);
        instantEffectObj.name = "Effect" + effectPool.Count;
        ParticleSystem instantEffect = instantEffectObj.GetComponent<ParticleSystem>();
        effectPool.Add(instantEffect);

        GameObject instantDongleObj = Instantiate(donglePrefab, dongleGroup);
        instantDongleObj.name = "Dongle" + donglePool.Count;
        Dongle instantDongle = instantDongleObj.GetComponent<Dongle>();
        instantDongle.effect = instantEffect;

        instantDongle.manager = this;
        donglePool.Add(instantDongle);

        return instantDongle;
    }

    void NextDongle()
    {
        if(isOver)
        {
            return;
        }

        var tempDongle = GetDongle();
        lastDongle = tempDongle;
        lastDongle.level = Random.Range(0,maxLevel); // max값은 포함되지 않음 0~7사이의 값
        lastDongle.gameObject.SetActive(true);
        SfxPlay(SfxType.Next);
        StartCoroutine(WaitNext());
    }
    IEnumerator WaitNext()
    {
        while(lastDongle != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);
        NextDongle();
    }

    Dongle GetDongle()
    {
        for(int index = 0; index < donglePool.Count; index++)
        {
            poolCursor = (poolCursor + 1 )% donglePool.Count;
            if(!donglePool[poolCursor].gameObject.activeSelf)
            {
                return donglePool[poolCursor];
            }
        }
        return MakeDongle();
    }
   

    public void TouchDown()
    {
        if(lastDongle == null)
        return;
        
        lastDongle.Drag();
    }
    public void TouchUp()
    {
        if(lastDongle == null)
        return;

        lastDongle.Drop();
        lastDongle = null;
    }

    public void GameOver()
    {
        if(isOver)
        return;

        isOver = true;
        // 장면안에 활성화되어 있는 모든 동글 가져오기
        StartCoroutine("GameOverRoutine");

    }

    IEnumerator GameOverRoutine()
    {
        Dongle[] dongles = GameObject.FindObjectsOfType<Dongle>();

        // 물리효과 끄기 => 델리게이트로 변경해보기
        for(int index = 0; index < dongles.Length; index++)
        {
            dongles[index].rigid.simulated = false;
        }

        for(int index = 0; index < dongles.Length; index++)
        {
            dongles[index].Hide(Vector3.up * 100);
            
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        // 최고점수 갱신
        int maxScore = Mathf.Max(score, PlayerPrefs.GetInt("MaxScore"));        
        PlayerPrefs.SetInt("MaxScore",maxScore);
        // 게임오버 UI
        subScoreText.text = "점수 : " + scoreText.text;
        endGroup.SetActive(true);

        bgmPlayer.Stop();
        SfxPlay(SfxType.Over);
    }
    public void Reset()
    {
        SfxPlay(SfxType.Button);
        StartCoroutine("ResetCoroution");

    }
    IEnumerator ResetCoroution()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }


    public void SfxPlay(SfxType type)
    {
        switch(type)
        {
            case SfxType.LevelUp:
            sfxPlayer[sfxCursor].clip = sfxClip[Random.Range(0,3)];
            break;

            case SfxType.Next:
            sfxPlayer[sfxCursor].clip = sfxClip[3];
            break;

            case SfxType.Attach:
            sfxPlayer[sfxCursor].clip = sfxClip[4];
            break;

            case SfxType.Button:
            sfxPlayer[sfxCursor].clip = sfxClip[5];
            break;

            case SfxType.Over:
            sfxPlayer[sfxCursor].clip = sfxClip[6];
            break;
        }
        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }

    private void Update() {
        
        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    private void LateUpdate() 
    {
        scoreText.text = score.ToString();
    }
}
