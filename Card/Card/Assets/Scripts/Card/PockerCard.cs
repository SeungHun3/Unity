using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class PockerCard : CardBase, IPointerClickHandler
{
    public TextMeshProUGUI Content_Text;
    public string SetText { set { Content_Text.text = value; } }
    public float PosX;

    public void SetCard(Transform spawn )
    {
        transform.position = spawn.position;
        
    }
    public void Open(float x, float y, float delay)
    {
        transform.localScale = new Vector3(0.5f, 0.4f, 1f);
        PosX = x;
        transform.DOScale(0.5f, 0.2f);
        transform.DOMoveY(y, delay);
    }
    
    public void MoveX(float delay)
    {
        transform.DOMoveX(PosX, delay);
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
