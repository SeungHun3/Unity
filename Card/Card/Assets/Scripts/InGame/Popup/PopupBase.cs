using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    public Popup_Animation AnimTarget;
    private bool _bIsAnimEnd;
    protected string _animName;

    public virtual IEnumerator Open()
    {
        gameObject.SetActive(true);
        yield return StartAnim();
    }

    IEnumerator StartAnim()
    {
        AnimTarget.StartAnim(this, _animName);
        while (!_bIsAnimEnd)
        {
            yield return null;
        }
        _bIsAnimEnd = false;
    }
    public virtual void EndAnim()
    {
        _bIsAnimEnd = true;
    }
}
