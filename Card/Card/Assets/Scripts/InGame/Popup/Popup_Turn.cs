using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Turn : PopupBase
{
    public GameObject Player;
    public GameObject Monster;

    public override IEnumerator Open()
    {
        _animName = "Popup_Turn";
        ShowTarget(TurnController.Instance.IsPlayerTurn);
        yield return base.Open();
        gameObject.SetActive(false);
    }

    public void ShowTarget(bool isPlayer)
    {
        gameObject.SetActive(true);
        Player.SetActive(isPlayer);
        Monster.SetActive(!isPlayer);

    }

}
