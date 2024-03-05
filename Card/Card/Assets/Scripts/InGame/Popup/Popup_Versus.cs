using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Versus : PopupBase
{
    public override IEnumerator Open()
    {
        _animName = "Versus";
        yield return base.Open();
        Destroy(gameObject);
    }
}
