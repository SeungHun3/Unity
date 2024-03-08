using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public delegate void StartDelegate(int spinCount);
public delegate void DownDelegate(float delta);
public delegate void StopDelegate();
public delegate void StateDelegate(Symbol.EState estate);

public class Reel : MonoBehaviour
{
    [HideInInspector]
    public string _name = string.Empty;
    // Slot Delegate
    StartDelegate _startDelegate;
    DownDelegate _spinningDelegate;
    StopDelegate _stopDelegate;
    StateDelegate _stateDelegate;

    public GameObject Prefab_Symbol;

    [HideInInspector]
    public ulong Property;
    [HideInInspector]
    public int SlotCount;
    [HideInInspector]
    public List<float> OriginPosY;
    [HideInInspector]
    public List<Symbol> Symbols;
    int _targetIndex;
    float _slotHeight;


    float _resetPosY;
    float _lineY;
    [HideInInspector]
    public bool IsSpinning;
    [HideInInspector]
    public float StopDelay;
    [HideInInspector]
    public int SpinCount;
    [HideInInspector]
    public float Speed;
    float _curSpeed;



    public void Init(string name, int slotCount, float speed, int stopCount)
    {
        _name = name;
        SlotCount = slotCount;
        Speed = speed;
        SpinCount = stopCount;

        RillSetting();
    }
    void RillSetting()
    {
        // 슬롯포지션, 슬롯생성

        _slotHeight = Prefab_Symbol.GetComponent<RectTransform>().rect.height;
        if (SlotCount % 2 == 0)
        {
            SlotCount++;
        }

        for (int i = 1; i <= SlotCount; i++)
        {
            if (SlotCount % 2 == 1)
            {
                OriginPosY.Add((i - (SlotCount / 2 + 1)) * _slotHeight);
            }
        }
        _lineY = -(SlotCount / 2 + 0.5f) * _slotHeight;
        _resetPosY = OriginPosY[OriginPosY.Count - 1] + _slotHeight * 0.5f;

        _targetIndex = SlotCount / 2; // 디버깅용
        MakeSymbols();
    }

    public void MakeSymbols()
    {
        for (int i = 0; i < OriginPosY.Count; i++)
        {
            Vector3 InitPos = new Vector3(0, OriginPosY[i], 0);
            Symbols.Add(Instantiate(Prefab_Symbol, transform).GetComponent<Symbol>());
            Symbols[i].transform.position = InitPos;
            Symbols[i].name = " Slot :" + _name + i;
            Symbols[i].Init(InitPos, _resetPosY, _lineY);

            _startDelegate += Symbols[i].SymbolStart;
            _spinningDelegate += Symbols[i].Spin;
            _stopDelegate += Symbols[i].SetLocation;
            _stateDelegate += Symbols[i].SetState;

        }
        _stopDelegate();

    }


    public void StartSpin(ulong property)
    {
        Property = property;
        _curSpeed = Speed;
        _startDelegate(SpinCount);
        _stateDelegate(global::Symbol.EState.Spinning);
        IsSpinning = true;
    }

    void SpinStop()
    {
        IsSpinning = false;
        _stopDelegate();
        SlotMachine.Instance.IsAllSpinStop();
    }

    void Update()
    {
        if (!IsSpinning)
            return;

        float delta = Time.deltaTime;

        // 속도
        if (Symbols[_targetIndex].SpinCount == 3)
        {
            _stateDelegate(global::Symbol.EState.Idle);
            _curSpeed = Mathf.Lerp(_curSpeed, 30f, delta);
        }
        if (Symbols[_targetIndex].SpinCount == 2)
        {
            _curSpeed = Mathf.Lerp(_curSpeed, 10f, delta);
        }

        // 타겟 슬롯 텍스쳐 세팅
        if (Symbols[_targetIndex].SpinCount < 0 && _curSpeed > 0)
        {
            Symbols[_targetIndex].SetTexture(Property);
            _curSpeed = Mathf.Lerp(_curSpeed, 5f, delta);
            if (Symbols[_targetIndex].transform.localPosition.y < -_slotHeight * 0.3f)
            {
                _curSpeed *= -1f;
                return;
            }
        }

        // 바운드
        if (_curSpeed < 0 && Symbols[_targetIndex].transform.localPosition.y > 0)
        {

            SpinStop();
            return;
        }

        _spinningDelegate(delta * _curSpeed);
    }


}
