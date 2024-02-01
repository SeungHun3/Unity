using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    
    public GameObject[] grenades;
    public int hasGrenades;
    public GameObject grenadeObj;
    public Camera FollowCamera;

    public int ammo;
    public int coin;
    public int health;
    
    public int Maxammo;
    public int Maxcoin;
    public int Maxhealth;
    public int MaxhasGrenades;



    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;
    bool iDown;
    bool fDown;
    bool gDown;
    bool rDown;

    bool sDown1;
    bool sDown2;
    bool IsJump;
    bool IsDodge;
    bool IsSwap;
    bool IsFireReady = true;
    bool IsReload;
    bool IsBorder;

    Rigidbody rigid;
    Vector3 moveVec;
    Vector3 DodgeVec;
    Animator anim;


    GameObject nearObject;
    Weapon equipWeapon;
    int equipWeaponIndex = -1;
    float fireDelay;


    
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Grenade();
        Attack();
        Reload();
        Dodge();
        Swap();
        Interation();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");
        gDown = Input.GetButton("Fire2");
        rDown = Input.GetButtonDown("Reload");
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if(IsDodge)
        {
            moveVec = DodgeVec;
        }
        if(IsSwap || IsReload)
        {
            moveVec = Vector3.zero;
        }

        if(!IsBorder)
        {
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        }

        anim.SetBool("IsRun",moveVec != Vector3.zero);
        anim.SetBool("IsWalk",wDown);
    }

    void Turn()
    {
        // 키보드 회전
        transform.LookAt(transform.position + moveVec);
        // 카메라 회전  //스크린에서 ray 쏘기
        if(fDown)
        {
            Ray ray = FollowCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0f;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }

    void Jump()
    {
        if(jDown && moveVec == Vector3.zero && !IsJump && !IsDodge && !IsSwap)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("IsJump",true);
            anim.SetTrigger("DoJump");
            IsJump = true;
        }
    }
    
    
    void Grenade()
    {
        if(hasGrenades == 0)
        return;
        if(gDown && !IsReload && !IsSwap)
        {
            Ray ray = FollowCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 2.0f;

                GameObject instantGrenade = Instantiate(grenadeObj, transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(nextVec * 5, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

                hasGrenades--;
                grenades[hasGrenades].SetActive(false);
            }
        }
    }
    void Attack()
    {
        if(equipWeapon == null)
        return;

        fireDelay += Time.deltaTime;
        IsFireReady = equipWeapon.rate < fireDelay;

        if(fDown && IsFireReady && !IsDodge && !IsSwap)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "DoSwing" : "DoShot");
            fireDelay = 0;
        }
    }
    
    void Reload()
    {
        if((equipWeapon == null) || (equipWeapon.type == Weapon.Type.Melee) || (ammo == 0))
        return;

        if(rDown && !IsJump && !IsDodge && !IsSwap)
        {
            anim.SetTrigger("DoReload");
            IsReload = true;
            Invoke("ReloadOut", 0.5f);
        }
    }

    void ReloadOut()
    {
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo;
        ammo -= reAmmo;
        IsReload = false;
    }

    void Dodge()
    {
        if(jDown && moveVec != Vector3.zero && !IsJump && !IsDodge && !IsSwap)
        {
            DodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("DoDodge");
            IsDodge = true;

            Invoke("DodgeOut", 0.4f);
        }
    }
    void DodgeOut()
    {
        speed *= 0.5f;
        IsDodge = false;
    }    
    
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Floor")
        {
            anim.SetBool("IsJump",false);
            IsJump = false;
        }
    }
    void Swap()
    {
        if(sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
        {
            return;
        }
        if(sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
        {
            return;
        }

        int weaponIndex = -1;
        if(sDown1) weaponIndex = 0;
        if(sDown2) weaponIndex = 1;

        if((sDown1 || sDown2)&& !IsJump && !IsDodge)
        {
            if(equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            Debug.Log(weaponIndex);
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("DoSwap");
            IsSwap = true;
            Invoke("SwapOut",0.4f);
        }
    }
    void SwapOut()
    {
        IsSwap = false;
    }  
    void Interation()
    {
        if(iDown && nearObject != null && !IsJump && !IsDodge)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;
                Destroy(nearObject);
            }
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch(item.type)
            {
                case Item.Type.Ammo:
                ammo += item.value;
                if(ammo > Maxammo)
                {
                    ammo = Maxammo;
                }
                break;

                case Item.Type.Coin:
                coin += item.value;
                if(coin > Maxcoin)
                {
                    coin = Maxcoin;
                }
                break;

                case Item.Type.Heart:
                health += item.value;
                if(health > Maxhealth)
                {
                    health = Maxhealth;
                }
                break;

                case Item.Type.Grenade:
                if(hasGrenades == MaxhasGrenades)
                {
                    return;
                }
                grenades[hasGrenades].SetActive(true);
                hasGrenades += item.value;
                
                break;

            }
        Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
    }

    void FreezeRotat()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void StopToWall()
    {
        IsBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
        //Debug.DrawRay(transform.position,transform.forward* 5f,Color.green);
    }
    void FixedUpdate() 
    {
        FreezeRotat();
        StopToWall();
    }

}
