using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    /* Serialized private fields */
    [SerializeField] private GameObject applePrefab = null;
    [SerializeField] private GameAreaManager gameAreaManager = null;

    /* Spawn an apple to start the game */
    void Start()
    {
        SpawnApple();
    }
    
    /* Instantiate an apple at a specified location on the grid */
    public void SpawnApple()
    {
        Instantiate(applePrefab, new Vector3(Random.Range(-gameAreaManager.cols / 2, gameAreaManager.cols / 2), Random.Range(gameAreaManager.rows / 2, -gameAreaManager.rows / 2), 0), Quaternion.identity);
    }
}
