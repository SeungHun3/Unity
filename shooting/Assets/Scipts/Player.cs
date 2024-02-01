using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // private 일경우 엔진에서 조정할 수 있는 메타데이터
    private float moveSpeed;
    

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;  
    [SerializeField]
    private float shootInterval = 0.05f;

    private float lastShotTime = 0f;  


   
    void Start()
    {
        
    }
    void Update()
    {
        // float horizontalInput  = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo= new Vector3(horizontalInput, verticalInput, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;
        
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime,0,0);
        // if(Input.GetKey(KeyCode.LeftArrow))
        // {
        //     transform.position -= moveTo;
        // }
        // else if(Input.GetKey(KeyCode.RightArrow))
        // {
        //     transform.position += moveTo;
        // }
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseX = mousePos.x + 15f;
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        float tochekc = Mathf.Clamp(mouseX, -2.35f, 2.35f);
        Debug.Log(tochekc);
        transform.position = new Vector3(tochekc, transform.position.y, transform.position.z );
        //Debug.Log(mousePos.x);
        if(!GameManager.instance.isGameOver)
        {
            Shoot();
        }
        //Debug.Log(transform.position);

    }

    
    void Shoot()
    {   
         if(Time.time - lastShotTime > shootInterval)
        {
            Instantiate(weapons[weaponIndex], shootTransform.position,Quaternion.identity);
            lastShotTime = Time.time;
        }
    }   
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //string tag = other.gameObject.tag;
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            GameManager.instance.SetGameOver(true);
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade()
    {
        weaponIndex += 1;
        if(weaponIndex >= weapons.Length)
        {
            weaponIndex = weapons.Length -1;
        }

    }
}