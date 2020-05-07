using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkEnemySpawnPoint : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float spawnRate;
    public int batchSpawnCount;
    public float batchRate;
    public string ObjectToSpawnStr;


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
                GameObject go = PhotonNetwork.Instantiate(ObjectToSpawnStr, this.transform.position, Quaternion.identity, 0);
                go.transform.parent = null;
            }
        }

    }
}
