using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    /* Serialized private fields */
    [SerializeField] private GameObject applePrefab = null;
    [SerializeField] private GridManager gridManager = null;

    /* Spawn an apple to start the game */
    void Start()
    {
        SpawnApple();
    }
    
    /* Instantiate an apple at a specified location on the grid */
    public void SpawnApple()
    {
        Instantiate(applePrefab, new Vector3(Random.Range(-gridManager.cols / 2, gridManager.cols / 2), Random.Range(gridManager.rows / 2, -gridManager.rows / 2), 0), Quaternion.identity);
    }
}
