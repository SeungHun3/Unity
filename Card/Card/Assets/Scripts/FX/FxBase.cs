using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FxBase : MonoBehaviour
{
    public ParticleSystem Particle;
    public float AnimTime = 1f;
    private void OnEnable() 
    {
        Particle.Play();
        Invoke("OffActive", AnimTime);
    }
    void OffActive()
    {
        gameObject.SetActive(false);
    }
}
