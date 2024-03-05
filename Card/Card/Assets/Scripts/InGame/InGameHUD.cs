using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameHUD : MonoBehaviour
{
    [Header("---------StatHUD--------")]
    public StatHUD PlayerHUD;
    public StatHUD EnemyHUD;
    [Header("----------Coin----------")]
    public FXSpawner CoinSpawner;
    public TextMeshProUGUI Coin_Text;
    int Coin;
    [Header("------Player Text Fx-----")]
    public TextSpawner Player_HPTextSpawner;
    public FXSpawner Player_HitFxSpawner;
    [Header("----------Card---------")]
    public SlotCardSpawner SlotCardSpawner;
    public PockerCardSpawner PockerCardSpawner;
    [Header("----------History---------")]
    public HistorySpawner HistorySpawner;



    public void Open()
    {
        Player player = TurnController.Instance.Player;
        Monster monster = TurnController.Instance.Monster;
        PlayerHUD.Open(player);
        EnemyHUD.Open(monster);

        Coin = player.Coin;
        Coin_Text.text = "$" + Coin.ToString();

        GetComponent<Animation>().Play("Hud_Create");
    }
    public void MachineOpen()
    {
        SlotMachine.Instance.Open();
    }


    public IEnumerator UpdateCoin()
    {
        int playerCoin = TurnController.Instance.Player.Coin;
        while (Coin < playerCoin)
        {
            Coin += 1;
            Coin_Text.text = "$" + Coin.ToString();
            yield return null;
        }
    }


    public IEnumerator CloseHud()
    {
        while (gameObject.GetComponent<CanvasGroup>().alpha > 0)
        {
            gameObject.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            yield return null;
        }

    }

}
