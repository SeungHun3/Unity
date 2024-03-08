using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Miss : PopupBase
{
    public override IEnumerator Open()
    {
        _animName = "Popup_Miss";
        yield return base.Open();
        //gameObject.SetActive(false);
    }
    private void OnEnable() {
        
    }
}
