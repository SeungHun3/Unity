using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void Awake()
    {
        base.Awake();

    }

    protected IEnumerator Load() // 컨트롤러에서 로드 시킬 예정
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
        //yield return PopupManager.Instance.OpenVersus();



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
