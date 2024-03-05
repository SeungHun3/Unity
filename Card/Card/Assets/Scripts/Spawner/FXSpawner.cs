using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FXSpawner : Spawner<FxBase>
{
    protected override FxBase Spawn()
    {
        return base.Spawn();
    }

    public void SpawnFX()
    {
        Spawn();
    }
    protected override void Awake() {
        base.Awake();
        _cnt = 1;
    }
}
