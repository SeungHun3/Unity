using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTable;

public struct CardInfo 
{
    public string IconName;
    public string SkillName;
    public string Content;
}

public class InGameTable : SingleDestroy<InGameTable>
{
    DataTableLoader _dataTableLoader = null;
    public Dictionary<ulong, CardInfo> CardInfo = new();

    public void InitTable()
    {
        _dataTableLoader = new DataTableLoader(Application.dataPath + "/DataTable");
        TempDataTable_List drawTableList = _dataTableLoader.GetDataTableList(TempDataTable_List.NAME) as TempDataTable_List;
        var keys = drawTableList.DataList.Keys;

        foreach (ulong key in keys)
        {
            TempDataTable row = _dataTableLoader.GetDataTable(TempDataTable_List.NAME, key) as TempDataTable;


            // AtlasManager
            {
                SlotInfo slotInfo = new SlotInfo(row.IconName);
                AtlasManager.Instance.SymbolsInfo[key] = slotInfo;
                AtlasManager.Instance.Symbolskill[key] = row.IconName;
            }
            // DrawManager
            {
                if (true)// 고정확률
                {
                    DrawManager.Instance.TotalFix += row.percentage;
                    DrawManager.Instance.FixList.Add((key, row.percentage));
                }
                else
                {
                    //DrawManager.Instance.NonFixList.Add(key);
                }
            }
            // CardManager
            {
                CardInfo cardInfo = new CardInfo()
                {
                    IconName = row.IconName,
                    SkillName = row.SkillName,
                    Content = row.Content1
                };
                CardInfo[key] = cardInfo;
            }
        }

    }
}
