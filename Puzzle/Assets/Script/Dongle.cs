using System.Collections;
using Unity.Mathematics;
using UnityEngine;
public class Dongle : MonoBehaviour
{
    public GameManager manager;
    public ParticleSystem effect;
    public int level;
    public bool IsDrag;
    public bool IsMerge;
    public bool IsAttach;
    public Rigidbody2D rigid;
    CircleCollider2D circle;
    Animator anim;
    float deadTime;
    SpriteRenderer spriteRenderer;
    private void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable() 
    {
        anim.SetInteger("Level",level);    
    }

    private void OnDisable() 
    {
        level = 0;
        IsDrag = false;
        IsAttach = false;
        IsMerge = false;

        transform.localPosition = Vector3.zero; // 그룹내부에 존재하기때문
        transform.localRotation = quaternion.identity;
        transform.localScale = Vector3.zero;

        rigid.simulated = false;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        circle.enabled = true;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if(IsDrag)
        {
            
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float leftBorder = -4.2f + transform.localScale.x / 2f;
        float RighthBorder = 4.2f - transform.localScale.y / 2f;
        if(mousePos.x < leftBorder)
        {
            mousePos.x = leftBorder;
        }
        else if(mousePos.x > RighthBorder)
        {
            mousePos.x = RighthBorder;
        }

        mousePos.y = 8;
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position,mousePos,0.2f);
        }
        
    }
    public void Drag()
    {
        IsDrag = true;
    }
    public void Drop()
    {
        IsDrag = false;
        rigid.simulated = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        
        StartCoroutine("AttachRoutine");
    }


    IEnumerator AttachRoutine()
    {
        if(IsAttach)
        {
            yield break;
        }
        IsAttach = true;
        manager.SfxPlay(GameManager.SfxType.Attach);
        yield return new WaitForSeconds(0.2f);
        IsAttach = false;
    }
    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Dongle")
        {
            Dongle dongleOther = other.gameObject.GetComponent<Dongle>();
            if(level == dongleOther.level && !IsMerge && !dongleOther.IsMerge && (level < 7))
            {
                // 합치기. 위치가져오기
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = dongleOther.transform.position.x;
                float otherY = dongleOther.transform.position.y;

                // 내가 아래에 있을때, 동일한 높이일때 = 내가 오른쪽에 있을때
                if(meY < otherY || (meY == otherY && meX > otherX))
                {
                    //나는 레벨업, 상대방은 숨기기
                    dongleOther.Hide(transform.position);
                    LevelUp();
                }
                
            }
        }
    }

    public void Hide(Vector3 targetPos)
    {
        IsMerge = true;
        rigid.simulated = false;
        circle.enabled = false;
        if(targetPos == Vector3.up * 100)
        {
            EffectPlay();
        }

        StartCoroutine(HideRoutine(targetPos));

    }
    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;
        while(frameCount < 20)
        {
            frameCount++;
            if(targetPos != Vector3.up * 100)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale,Vector3.zero, 0.2f);
            }

            yield return null;
        }
        manager.score += (int)Mathf.Pow(2,level);
        IsMerge = false;
        gameObject.SetActive(false);
    }
    void LevelUp()
    {
        IsMerge = true;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0; // 회전속도
        StartCoroutine(LevelUpRoutine());
    }
    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level +1);
        EffectPlay();
        manager.SfxPlay(GameManager.SfxType.LevelUp);
        yield return new WaitForSeconds(0.3f);

        level++;
        manager.maxLevel = Mathf.Max(level, manager.maxLevel);
        Debug.Log(manager.maxLevel);
        IsMerge = false;
    }

    void EffectPlay()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.localScale;
        effect.Play();
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Finish")
        {
            deadTime += Time.deltaTime;
            if(deadTime > 2)
            {
                spriteRenderer.color = new Color(1,0,0);
            }
            if(deadTime > 5)
            {
                manager.GameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Finish")
        {
            deadTime = 0;
            spriteRenderer.color = Color.white;
        }
    }

}
