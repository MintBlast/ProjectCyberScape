using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Update is called once per frame
    void FixedUpdate()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
