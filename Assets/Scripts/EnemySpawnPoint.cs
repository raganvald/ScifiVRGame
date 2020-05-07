using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float spawnRate;
    public int batchSpawnCount;
    public float batchRate;


    public void Awake()
    {
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(batchRate);
            for (int i=0; i < batchSpawnCount; i++)
            { 
                yield return new WaitForSeconds(spawnRate);
                GameObject go = Instantiate(ObjectToSpawn, this.transform);
                go.transform.parent = null;
            }
        }

    }
}
