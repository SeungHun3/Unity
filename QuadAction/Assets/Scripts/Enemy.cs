using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int MaxHealth;
    public int CurHealth;
    public bool IsChase;
    public Transform target;

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;


    void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();    
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        Invoke("ChaseStart",3);
    }
    void ChaseStart()
    {
        IsChase = true;
        anim.SetBool("IsWalk", true);
    }

    void Update()   
    {
        if(IsChase)
        {
           nav.SetDestination(target.position);
        }
    }



    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            CurHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec,false));
        }   
        else if(other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            CurHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            StartCoroutine(OnDamage(reactVec,false));
        } 
    }
    public void HitByGrenade(Vector3 explosionPos)
    {
        CurHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec,true));
    }



    IEnumerator OnDamage(Vector3 reactVec, bool IsGrenade)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(CurHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 13;
            IsChase = false;
            nav.enabled = false;
            anim.SetTrigger("DoDie");

            if(IsGrenade)
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up * 3;
                rigid.freezeRotation = false;
                rigid.AddForce(reactVec * 5 , ForceMode.Impulse);
                rigid.AddTorque(reactVec *15, ForceMode.Impulse);
            }
            else    
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec * 5 , ForceMode.Impulse);
            }

            Destroy(gameObject, 4);
        }

    }
    void FreezeVelocity()
    {
        if(IsChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    
    void FixedUpdate() 
    {
        FreezeVelocity();
    }

}
