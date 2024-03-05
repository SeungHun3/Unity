using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MoveX_Delegate(float delay);

public class PockerCardSpawner : Spawner<PockerCard>
{
    public Transform SpawnPos;
    public List<Transform> ArrivedPos = new();

    public MoveX_Delegate MoveX;

    protected override void Awake()
    {
        base.Awake();
        _cnt = 1;
    }
    protected override void Start()
    {
        base.Start();
    }

    public IEnumerator OpenPockerCard()
    {
        for (int i = 0; i < 3; i++)
        {
            PockerCard pockerCard = Spawn();
            pockerCard.transform.SetParent(SpawnPos);
            pockerCard.SetCard(SpawnPos);
            pockerCard.Open(ArrivedPos[i].position.x, ArrivedPos[1].position.y, 1f);
            MoveX += pockerCard.MoveX;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield return new WaitForSecondsRealtime(1f);
        MoveX(1f);

        yield return new WaitForSecondsRealtime(1f);

    }
}
