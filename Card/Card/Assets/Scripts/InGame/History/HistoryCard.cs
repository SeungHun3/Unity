using System.Collections;
using System.Collections.Generic;
using EnumTypes.InGame;
using UnityEngine;
using UnityEngine.UI;

public class HistoryCard : CardBase
{
    public Image MainImage;
    public Image BackGround;

    public void SetCard(ESlotCard eSlotCard, bool isPlayer)
    {
        AtlasManager AtlasMgr = AtlasManager.Instance;
        Dictionary<ulong, CardInfo> cardInfo = InGameTable.Instance.CardInfo;
        
        MainImage.sprite = AtlasMgr.GetSlotSkillSprite(cardInfo[(ulong)eSlotCard].IconName);
        string bgname = "History_Bg_";
        bgname += isPlayer ? "User" : "Enemy";
        BackGround.sprite = AtlasMgr.GetHudSprite(bgname);
    }
}
