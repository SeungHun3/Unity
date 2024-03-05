using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Animation : MonoBehaviour
{
    PopupBase _target;
    public void StartAnim(PopupBase target, string animName)
    {
        _target = target;
        GetComponent<Animation>().Play(animName);
    }
    public void EndAnim()
    {
        _target.EndAnim();
    }
}
