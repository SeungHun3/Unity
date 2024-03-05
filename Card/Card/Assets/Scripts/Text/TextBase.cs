using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBase : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public string AnimName;
    public string SetText { set { TMP.text = value; } }
    public Color SetColor { set { TMP.color = value; } }

    private void OnEnable()
    {
        GetComponent<Animation>().CrossFade(AnimName);
        Invoke("OffActive", 1f);
    }

    void OffActive()
    {
        gameObject.SetActive(false);
    }
}
