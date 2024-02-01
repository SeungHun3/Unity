using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range, };
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform BulletPos;
    public GameObject Bullet;
    public Transform BulletCasePos;
    public GameObject BulletCase;
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing"); // 대기중에도 중지
            StartCoroutine("Swing");

        }
        else if(type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }



    IEnumerator Swing()
    {
        // 1 로직실행
        yield return new WaitForSeconds(0.1f);  // 0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        // 2 로직실행
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
        yield break; // 코루틴 탈출
    }
    // Use()  == 메인루틴 -> Swing() 서브 루틴 -> Use()메인루틴
    // 코루틴 : Use()메인루틴 + Swing() 코루틴(co - Op)

    IEnumerator Shot()
    {
        // 1 총알발사
        GameObject instantBullet = Instantiate(Bullet, BulletPos.position,BulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = BulletPos.forward * 50f;
        yield return null;  
        // 2 탄피배출
        GameObject instantCase = Instantiate(BulletCase, BulletCasePos.position,BulletCasePos.rotation);
        Rigidbody CaseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = BulletCasePos.forward * Random.Range(-3,-2) + Vector3.up * Random.Range(2,3);
        CaseRigid.AddForce(caseVec, ForceMode.Impulse);
        CaseRigid.AddTorque(Vector3.up * 10f,ForceMode.Impulse);
    }
}
