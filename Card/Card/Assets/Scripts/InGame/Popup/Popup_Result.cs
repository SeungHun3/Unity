using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Popup_Result : PopupBase
{
    public override IEnumerator Open()
    {
        _animName = TurnController.Instance.IsPlayerTurn ? "Result_Defeat": "Result_Victory";
        yield return base.Open();
    }
    public void EndBTN()
    {
        SceneManager.LoadScene("InGame");
    }
}
