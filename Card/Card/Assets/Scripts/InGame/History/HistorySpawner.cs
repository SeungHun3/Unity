using System.Collections;
using System.Collections.Generic;
using EnumTypes.InGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HistorySpawner : Spawner<HistoryCard>
{
    public RectTransform Content;

    bool _isToggleOn;

    protected override void Awake()
    {
        base.Awake();
        _cnt = 0;
        _isToggleOn = false;
        Content.sizeDelta = new Vector2(0f, 5f);
    }

    public void AddCard(ESlotCard eSlotCard, float padding = 5f) // player?enemy , texture
    {
        Vector2 curSize = Content.sizeDelta;
        curSize.y += Prefab.GetComponent<RectTransform>().sizeDelta.y + padding; // size + padding
        Content.sizeDelta = curSize;

        HistoryCard card = Spawn();
        card.transform.SetParent(Content);
        card.SetCard(eSlotCard, TurnController.Instance.IsPlayerTurn);
    }


    public void SetToggle()
    {
        if (_isToggleOn)
        {
            _isToggleOn = false;
            GetComponent<Animator>().SetTrigger("Close");
            Debug.Log("History Close");
        }
        else
        {
            _isToggleOn = true;
            GetComponent<Animator>().SetTrigger("Open");
            Debug.Log("History Open");
        }
    }
}
