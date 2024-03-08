using System.Collections;
using System.Collections.Generic;
using EnumTypes.InGame;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;



public class Symbol : MonoBehaviour
{
    Vector3 _initPos;
    string _curName;
    EState _curState;

    public string debug;
    public int serial_debug;

    public enum EState
    {
        Idle,
        Spinning,
        Win,
        Finish
    }

    [HideInInspector]
    public float ResetY;
    [HideInInspector]
    public float LineY;
    [HideInInspector]
    public int SpinCount;
    float _imageSize;
    [HideInInspector]
    public int Count;

    public void SetTexture(ulong serialNum) // DataTable내에 존재하는 Serial Number의 값
    {
        _curName = AtlasManager.Instance.SymbolsInfo[serialNum].Name;
        string curstate = string.Empty;
        switch (_curState)
        {
            case EState.Idle:
                curstate = AtlasManager.Instance.SymbolsInfo[serialNum].Idle;
                break;
            case EState.Spinning:
                curstate = AtlasManager.Instance.SymbolsInfo[serialNum].Move;
                break;
            case EState.Win:
                curstate = AtlasManager.Instance.SymbolsInfo[serialNum].Idle;
                break;
        }
        GetComponent<Image>().sprite = AtlasManager.Instance.GetSymbolsprite(_curName + curstate);
        debug = _curName + curstate;
        serial_debug = (int)serialNum;
    }

    public void SetState(EState estate)
    {
        _curState = estate;
    }

    public void Init(Vector3 Pos, float resetY, float lineY)
    {
        _initPos = Pos;
        ResetY = resetY;
        LineY = lineY;
        _imageSize = GetComponent<RectTransform>().rect.height;

        SetState(EState.Idle);

    }


    public void SymbolStart(int spinCount)
    {
        SpinCount = spinCount;
    }

    public void Spin(float _delta)
    {
        Vector3 NewPos = transform.localPosition;
        NewPos.y -= _delta * _imageSize * 0.1f;
        transform.localPosition = NewPos;
    }
    public void SetLocation()
    {
        transform.localPosition = _initPos;
    }



    private void Update()
    {
        if (transform.localPosition.y < LineY)
        {
            Vector3 newPos = transform.localPosition;
            newPos.y = ResetY + (transform.localPosition.y - LineY); // 리셋 + 프레임 보간
            transform.localPosition = newPos;
            SetTexture((ulong)Random.Range((ulong)ESlotCard.None + 1, (ulong)ESlotCard.End));
            SpinCount--;
        }
    }


    private void Start() // Serial Number : 1 ~
    {
        SetTexture((ulong)Random.Range((ulong)ESlotCard.None + 1, (ulong)ESlotCard.End));
    }

}
