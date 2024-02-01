using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Backgroud : MonoBehaviour
{
    private float movespeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * movespeed * Time.deltaTime;
        if(transform.position.y < -10f)
        {
            transform.position += new Vector3(0f,20f,0f);
        }
    }
}
