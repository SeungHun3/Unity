using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : SingleDestroy<Handle>
{
    public GameObject Head;
    public GameObject Neck;
    public Transform HeadArrived;
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
        GetComponent<Animation>().CrossFade("SlotHandle_Up");
        SlotMachine.Instance.Play();
    }
    public void ResetBTN()
    {
        bInput = true;
    }

    public void DragInput()
    {
        if (!bInput)
            return;

        Vector3 head = Head.transform.position;
        head.y = Mathf.Clamp(Input.mousePosition.y, _headArrivedPos.y, _headStartPos.y);
        Head.transform.position = head;

        Vector3 neck = Neck.GetComponent<RectTransform>().anchoredPosition;
        float y = Head.GetComponent<RectTransform>().anchoredPosition.y + 14f;
        neck.y = Mathf.Clamp((y / 4f) + 10f, 10f, 40f);
        Neck.GetComponent<RectTransform>().anchoredPosition = neck;

        if (Input.mousePosition.y < _headArrivedPos.y)
        {
            bInput = false;
            GetComponent<Animation>().CrossFade("SlotHandle");
            SlotMachine.Instance.Play();
        }
    }

    

}
