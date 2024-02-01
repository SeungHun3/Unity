using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject gameOverPanel;
    //[SerializeField]
   // private GameObject StartBTN;

    [HideInInspector] // 에디터에서 숨기기
    public bool isGameOver = false;
    private int coin = 0;
    
    void Awake()  // Start() 보다 더 이전 함수 == Initialize()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseCoin()
    {
        coin += 1;
        text.SetText(coin.ToString());

        if(coin % 3 == 0)
        {
            Player player = FindObjectOfType<Player>();
            if(player != null)
            {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver(bool isWin)
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }
        if(isWin)
        {

        }
        isGameOver = true;
        Invoke("ShowGameOverPanel",1f);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }


    private void Start() 
    {
        
    }


}