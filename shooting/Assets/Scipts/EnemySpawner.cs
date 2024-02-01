using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;
    private float[] arrPosX = { -2.2f, -1.1f, 0f, 1.1f, 2.2f};
    [SerializeField]
    private float spawnInterval = 1.5f;

    void Start()
    {
        StartEnemyRoutine();
    }
    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine"); // 중단되거나 일시 중단된 상태에서 다시 시작할 수 있는 함수
    }
    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3f); // 첫시작 기다렸다가

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while(true)
        {
            foreach( float posX in arrPosX)
            {
                //int randIndex = Random.Range(0,enemies.Length);
                SpawnEnemy(posX,enemyIndex, moveSpeed);
            }
            spawnCount++;
            if(spawnCount % 10 == 0)
            {
                enemyIndex++;
                moveSpeed += 2f;
            }
            if(enemyIndex >= 1)//enemies.Length)
            {
                SpawnBoss();
                //StopEnemyRoutine();
                //yield return null;
                yield break;
                //Debug.Log("enemy1");
                //Debug.Log("enemy2");
                //SpawnEnemy(0,0, moveSpeed);
                //SpawnEnemy(0,0, moveSpeed);
                
            }
            yield return new WaitForSeconds(spawnInterval); // while wait 루프
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        if(Random.Range(0,5) == 0)
        {
            index += 1;
        }
        if(index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        GameObject enemyObject = Instantiate(enemies[index],spawnPos, Quaternion.identity); // 스폰함수
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        //Debug.Log("// Enemy : spawnPos"+ spawnPos);
        if(enemy)
        {
            enemy.SetMoveSpeed(moveSpeed);
        }
    }

    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
       
    }   
}
