using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemy;
    public float spawnTime = 15;

    private void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
