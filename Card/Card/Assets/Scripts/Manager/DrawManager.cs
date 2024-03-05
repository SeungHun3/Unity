using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTable;

public class DrawManager : SingleDontDestroy<DrawManager>
{
    public List<(ulong key, ulong percent)> FixList = new List<(ulong key, ulong percent)> { };
    public List<ulong> NonFixList = new List<ulong> { }; // serialNum
    public ulong TotalFix;
    //public int ItemCount;

    public ulong GetRandomCard()
    {
        ulong sum = 0;
        ulong random = (ulong)Random.Range(1, 101); // 1 ~ 100
        if (random <= TotalFix)
        {
            foreach (var x in FixList)
            {
                sum += x.percent;
                if (sum >= random)
                {
                    return x.key;
                }
            }
        }
        else
        {
            int index = Random.Range(1, NonFixList.Count);
            return NonFixList[index];
        }

        return 0;
    }


    void Temp()
    {
        
    }


}

