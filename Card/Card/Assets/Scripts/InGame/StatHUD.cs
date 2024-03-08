using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatHUD : MonoBehaviour
{
    public Slider Hp;
    public TextMeshProUGUI CurHP;
    public TextMeshProUGUI MaxHP;

    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Defence;

    public CharBase _target;


    public void Open(CharBase target)
    {
        _target = target;
        _target.HPDelegate += UpdateHP;
        
        MaxHP.text = ((int)_target.MaxHP).ToString();
        CurHP.text = ((int)_target.CurHP).ToString() + "/";
        Hp.value = 1f;
        Attack.text = _target.Attack.ToString();
        Defence.text = _target.Defence.ToString();
    }
    void UpdateHP()
    {
        CurHP.text = ((int)_target.CurHP).ToString() + "/";
        StartCoroutine(SetHP(Hp, _target.NormalizedHP));
    }

    public void SetAttack(int attack)
    {
        Attack.text = attack.ToString();
    }
    public void SetDefence(int defence)
    {
        Defence.text = defence.ToString();
    }

    IEnumerator SetHP(Slider target, float NormailizeHP)
    {
        while (0.01f < Mathf.Abs(target.value - NormailizeHP))
        {
            target.value = Mathf.Lerp(target.value, NormailizeHP, Time.deltaTime * 3f);
            yield return null;
        }
    }

}
