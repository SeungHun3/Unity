using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : SingleDestroy<PopupManager>
{
    public RectTransform Canvas;
    public GameObject Prefab_Hud;

    public GameObject Prefab_Versus;
    public GameObject Prefab_Turn;
    public GameObject Prefab_Result;
    public GameObject Prefab_Miss;

    public InGameHUD Inst_InGameHUD;
    public Popup_Turn Inst_Turn;
    public Popup_Miss Inst_Miss;
    public Popup_Result Inst_Result;

    public void InitHud()
    {
        Inst_InGameHUD = Instantiate(Prefab_Hud, Canvas.transform).GetComponent<InGameHUD>();
        Inst_InGameHUD.GetComponent<CanvasGroup>().alpha = 0;
        SlotMachine.Instance.Init();
    }
    public void InitPopups()
    {
        Inst_Turn = Instantiate(Prefab_Turn, Canvas.transform).GetComponent<Popup_Turn>();
        Inst_Turn.gameObject.SetActive(false);

        Inst_Miss = Instantiate(Prefab_Miss, Canvas.transform).GetComponent<Popup_Miss>();
        Inst_Miss.gameObject.SetActive(false);
    }
    public IEnumerator OpenVersus()
    {
        yield return Instantiate(Prefab_Versus, Canvas.transform).GetComponent<Popup_Versus>().Open();
        
    }

    public IEnumerator EndPopup()
    {
        yield return Inst_InGameHUD.CloseHud();
        yield return Instantiate(Prefab_Result, Canvas.transform).GetComponent<Popup_Result>().Open();
    }

}
