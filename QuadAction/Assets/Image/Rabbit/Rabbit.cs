using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Rabbit : MonoBehaviour
{
    public SkeletonGraphic skeletonRabbit;
    
    private void Start() {
    }

    public void HoverEnter()
    {
        skeletonRabbit.AnimationState.SetAnimation(1, "Win", true);
    }
    public void HoverExit()
    {
        skeletonRabbit.AnimationState.SetAnimation(1, "Idle", false);
    }
}
