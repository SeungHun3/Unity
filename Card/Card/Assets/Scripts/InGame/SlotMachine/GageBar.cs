using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageBar : SingleDestroy<GageBar>
{

    Slider _slider;
    float _animSpeed;


    protected override void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0f;
        _animSpeed = 3f;
    }

    
    public void SetValueCoroutine(float value)
    {
        StartCoroutine(SetValue(value));
    }
    public IEnumerator SetValue(float value)
    {
        while (0.01f < Mathf.Abs(_slider.value - value))
        {
            _slider.value = Mathf.Lerp(_slider.value, value, Time.deltaTime * _animSpeed);
            yield return null;
        }
    }

}
