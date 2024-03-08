using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mongs;
using Mongs.API;

public class InGameManager : SingleDestroy<InGameManager>
{
    public GameObject Monster_Obj;
    public TextSpawner Mon_HitTextSpawner;
    public FXSpawner Mon_HitFxSpawner;

    [Header("----------Player----------")]
    public int Player_Attack;
    public int Player_HP;
    public int Player_Defence;
    [Header("----------Monster----------")]
    public int Monster_Attack;
    public int Monster_HP;
    public int Monster_Defence;
    [Header("----------Delay----------")]
    [Range(0.0f, 2.0f)]
    public float MonsterPlayDelay;
    [Range(0.0f, 2.0f)]
    public float TurnDelay;
    protected override void Awake()
    {
        base.Awake();
        LoginProc loginProc = MongsWebNetManager.Instance.GetApi(MongsWebNetManager.API_TYPE.LoginProc) as LoginProc;
        loginProc.RequestData.PlatformID = "Test_PlatformID";
        loginProc.RequestData.PlatformType = Common.Enum.PlatformType.Guest;

        loginProc.Request();
    }

    protected IEnumerator Load() // 씬 컨트롤러에서 로드 시킬 예정
    {
        yield return AtlasManager.Instance.LoadAtlas();
    }
    void Init()
    {
        // temp
        InGameTable.Instance.InitTable();

        PopupManager.Instance.InitHud();
        PopupManager.Instance.InitPopups();

        TurnController.Instance.Init();
    }

    void LoadFinished()
    {
        AtlasManager.Instance.LoadFinished();
    }

    protected void Start()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return Load();// 컨트롤러에서 로드 시킬 예정


        Init();
        LoadFinished();
        yield return PopupManager.Instance.OpenVersus();



        TurnController.Instance.Monster.TriggerMotion(EnumTypes.InGame.EMotion.Arrival);
        yield return new WaitForSecondsRealtime(0.5f);
        PopupManager.Instance.Inst_InGameHUD.Open();
        yield return new WaitForSecondsRealtime(2f);
        TurnController.Instance.StartCoroutine(TurnController.Instance.StartGame());

    }

    public void GameEnd()
    {
        StartCoroutine(End());
    }
    IEnumerator End()
    {
        yield return PopupManager.Instance.EndPopup();
    }

}
