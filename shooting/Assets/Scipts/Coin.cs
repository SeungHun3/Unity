using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -7;
    void Start()
    {
        Jump();
    }
    void Jump()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        float randomJumpForce = Random.Range(4f,8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f,2f);
        rigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }


// void delayStopSimulate()
//     {
//         StartCoroutine("tempfuc"); // 중단되거나 일시 중단된 상태에서 다시 시작할 수 있는 함수
//     }
//     IEnumerator tempfuc()
//     {
//         yield return new WaitForSeconds(2f);

//         Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
//         rigidbody.simulated = false;
//     }
}
