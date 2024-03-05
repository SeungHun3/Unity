using System.Collections;
using System.Collections.Generic;
using EnumTypes.InGame;
using UnityEngine;

public class SlotMachine : SingleDestroy<SlotMachine>
{

    int _gageCount;

    public Reel Left;
    public Reel Centor;
    public Reel Right;
    public GameObject Shutter;
    public GameObject TurnArrow;

    #region 슬롯, 릴 
    [Range(0.0f, 2.0f)]
    public float ReelsDelay;
    [Range(50.0f, 600.0f)]
    public float Speed;
    [Range(5, 50)]
    public int SpinningNum;
    int _slotCount;

    [HideInInspector]
    public ulong Property;


    #endregion

    // 릴, 핸들, 게이지바
    public void Init()
    {
        Handle.Instance.Init();
        GetComponent<RectTransform>().localScale = Vector3.zero;

        _slotCount = 3;
        _gageCount = 0;
        // 릴 세팅
        Left.Init("Left", _slotCount, Speed, SpinningNum);
        Centor.Init("Centor", _slotCount, Speed, SpinningNum);
        Right.Init("Right", _slotCount, Speed, SpinningNum);

    }

    public void Open()
    {
        GetComponent<Animation>().Play("SlotMachine_Legacy");
    }

    public void ShutterAnim()
    {
        Shutter.GetComponent<Animator>().SetTrigger("Turn");
    }

    public void TurnChange()
    {
        TurnArrow.GetComponent<Animator>().SetTrigger(TurnController.Instance.IsPlayerTurn ? "User" : "Enemy");
        Handle.Instance.Head.GetComponent<Animator>().SetTrigger(TurnController.Instance.IsPlayerTurn ? "User" : "Enemy");
    }

    public void Play()
    {
        CharBase target = TurnController.Instance.Target;
        if (target.ESlotCard == ESlotCard.None)
        {
            while (true)
            {
                ulong a = (ulong)Random.Range((int)ESlotCard.None + 1, (int)ESlotCard.End);
                ulong b = (ulong)Random.Range((int)ESlotCard.None + 1, (int)ESlotCard.End);
                ulong c = (ulong)Random.Range((int)ESlotCard.None + 1, (int)ESlotCard.End);
                if (!(a == b && b == c))
                {
                    StartCoroutine(SpinStart(a, b, c));
                    break;
                }
            }
        }
        else
        {
            ulong result = (ulong)target.ESlotCard;
            StartCoroutine(SpinStart(result, result, result));
        }
    }


    IEnumerator SpinStart(ulong _leftProp, ulong _centorProp, ulong _rightProp)
    {
        Left.StartSpin(_leftProp);
        yield return new WaitForSecondsRealtime(ReelsDelay);
        Centor.StartSpin(_centorProp);
        yield return new WaitForSecondsRealtime(ReelsDelay);
        Right.StartSpin(_rightProp);
    }

    void SpinStopEvent()
    {
        if (TurnController.Instance.IsPlayerTurn)
        {
            PlayerStopEvent();
        }
        else
        {

        }
        TurnController.Instance.EndTurnCoroutine();
    }

    void PlayerStopEvent()
    {
        _gageCount += 1;
        if (_gageCount >= 10)
        {
            _gageCount = 0;
            GageBar.Instance.SetValueCoroutine(_gageCount * 0.1f);
        }
        else
        {
            GageBar.Instance.SetValueCoroutine(_gageCount * 0.1f);
        }
    }

    public bool IsAllSpinStop()
    {
        if (!Left.IsSpinning && !Centor.IsSpinning && !Right.IsSpinning)
        {
            SpinStopEvent();
            return true;
        }
        return false;
    }
}
