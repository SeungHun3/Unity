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
    ResourceRequest _reqSlot, _reqSymbolSkill, _reqHud;
    SpriteAtlas _slotAtlas;
    SpriteAtlas _SymbolSkillAtlas;
    SpriteAtlas _hudAtlas;
    public Dictionary<ulong, SlotInfo> SymbolsInfo = new Dictionary<ulong, SlotInfo>();
    public Dictionary<ulong, string> Symbolskill = new Dictionary<ulong, string>();

    public ResourceRequest RequestSlot => _reqSlot;
    public ResourceRequest RequestSymbolSkill => _reqSymbolSkill;
    public ResourceRequest RequestHud => _reqHud;

    public Sprite GetSymbolsprite(string name)
    {
        return _slotAtlas.GetSprite(name);
    }
    public Sprite GetSymbolSkillSprite(string name)
    {
        return _SymbolSkillAtlas.GetSprite(name);
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

        _reqSymbolSkill = Resources.LoadAsync<SpriteAtlas>("UI/Atlas/SymbolSkill");
        while (RequestSymbolSkill.progress < 1)
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
        _SymbolSkillAtlas = SetAtlas(_reqSymbolSkill);
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
