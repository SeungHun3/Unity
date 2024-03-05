using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using DataTable;
using UnityEngine.Localization.Tables;

public struct SlotInfo
{
    public string Name;
    public string Idle;
    public string Move;
    public string Win;

    public SlotInfo(string name)
    {
        Name = name;
        Idle = "_Idle";
        Move = "_Move";
        Win = "_Win";
    }
}
//Assets/Resources/UI/InGame/Popup_Versus_Result/Coin_Bg.png
//Assets/Resources/UI/InGame/Popup_Versus_Result/Coin_Bg.png
public class AtlasManager : SingleDontDestroy<AtlasManager>
{
    ResourceRequest _reqSlot, _reqSlotSkill, _reqHud;
    SpriteAtlas _slotAtlas;
    SpriteAtlas _slotSkillAtlas;
    SpriteAtlas _hudAtlas;
    public Dictionary<ulong, SlotInfo> SlotsInfo = new Dictionary<ulong, SlotInfo>();
    public Dictionary<ulong, string> SlotSkill = new Dictionary<ulong, string>();

    public ResourceRequest RequestSlot => _reqSlot;
    public ResourceRequest RequestSlotSkill => _reqSlotSkill;
    public ResourceRequest RequestHud => _reqHud;

    public Sprite GetSlotSprite(string name)
    {
        return _slotAtlas.GetSprite(name);
    }
    public Sprite GetSlotSkillSprite(string name)
    {
        return _slotSkillAtlas.GetSprite(name);
    }
    public Sprite GetHudSprite(string name)
    {
        return _hudAtlas.GetSprite(name);
    }


    public IEnumerator LoadAtlas()
    {
        _reqSlot = Resources.LoadAsync<SpriteAtlas>("UI/Atlas/Slot");
        while (RequestSlot.progress < 1)
        {
            yield return null;
        }

        _reqSlotSkill = Resources.LoadAsync<SpriteAtlas>("UI/Atlas/SlotSkill");
        while (RequestSlotSkill.progress < 1)
        {
            yield return null;
        }
        _reqHud = Resources.LoadAsync<SpriteAtlas>("UI/Atlas/Hud");
        while (RequestHud.progress < 1)
        {
            yield return null;
        }
    }

    public void LoadFinished()
    {
        _slotAtlas = SetAtlas(_reqSlot);
        _slotSkillAtlas = SetAtlas(_reqSlotSkill);
        _hudAtlas = SetAtlas(_reqHud);
    }

    SpriteAtlas SetAtlas(ResourceRequest request)
    {
        if (request.asset != null && request.asset is SpriteAtlas)
        {
            return (SpriteAtlas)request.asset;
        }
        return null;
    }

}
