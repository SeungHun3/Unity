using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Handle : SingleDestroy<Handle>
{
    public GameObject Head;
    public GameObject Neck;
    public Transform HeadArrived;
    [Range(0.01f, 1.0f)]
    public float ResetSpeed;
    public bool bInput = false;
    bool Option_IsClick;

    Vector3 _headStartPos;
    Vector3 _headArrivedPos;
    

    public void Init()
    {
        _headStartPos = Head.transform.position;
        _headArrivedPos = HeadArrived.position;
    }

    public void Click()
    {
        GetComponent<Animation>().Play("SlotHandle_Up");
        SlotMachine.Instance.Play();
    }

    public void DragInput()
    {
        if (!bInput)
            return;

        Vector3 head = Head.transform.position;
        head.y = Mathf.Clamp(Input.mousePosition.y, _headArrivedPos.y, _headStartPos.y);
        Head.transform.position = head;
        UpdateNeckPosition();


        if (Input.mousePosition.y < _headArrivedPos.y)
        {
            bInput = false;
            GetComponent<Animation>().Play("SlotHandle");
            SlotMachine.Instance.Play();
        }
    }

    public void Return()
	{
        StartCoroutine(ReturnAnim());
	}

    public IEnumerator ReturnAnim()
    {
        if (!bInput)
            yield break;

        bInput = false;
        while (Head.transform.position.y < _headStartPos.y)
        {
            Vector3 head = Head.transform.position;
            head.y += Mathf.Abs(_headStartPos.y - _headArrivedPos.y) * ResetSpeed;
            Head.transform.position = head;

            UpdateNeckPosition();
            yield return null;
        }
        Head.transform.position = _headStartPos;
        UpdateNeckPosition();
        GetComponent<Animation>().Play("SlotHandle_Bound");
        bInput = true;
    }

    void UpdateNeckPosition()
	{
        Vector3 neck = Neck.GetComponent<RectTransform>().anchoredPosition;
        float y = Head.GetComponent<RectTransform>().anchoredPosition.y + 14f;
        neck.y = Mathf.Clamp((y / 4f) + 10f, 10f, 40f);
        Neck.GetComponent<RectTransform>().anchoredPosition = neck;
    }

}
