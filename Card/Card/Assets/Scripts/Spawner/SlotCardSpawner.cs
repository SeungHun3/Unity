using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes.InGame;



public class SlotCardSpawner : Spawner<SlotCard>
{
    [Header("-------Position------")]
    public Transform SpawnPos;
    public Transform Arrived_1;
    public Transform Arrived_2;

    [Header("-------Velocity------")]
    public float V_Arrived_1;
    public float V_Arrived_2;
    public float V_Scale;
    [Header("--------Delay--------")]
    public float D_Arrived_1;
    public float D_Arrived_2;
    public float D_Scale;


    protected override void Awake()
    {
        base.Awake();
        _cnt = 1;
    }

    public IEnumerator OpenSlotCard(ESlotCard eSlotCard)
    {
        if (eSlotCard == ESlotCard.None)
        {
            yield return PopupManager.Instance.Inst_Miss.Open();
            yield break;
        }

        SlotCard slotCard = Spawn();
        slotCard.transform.SetParent(SpawnPos.transform);
		slotCard.SetCard(eSlotCard);
		yield return slotCard.Open(SpawnPos.transform, Arrived_1, V_Arrived_1, D_Arrived_1, Arrived_2, V_Arrived_2, D_Arrived_2, V_Scale, D_Scale);
	}


}





