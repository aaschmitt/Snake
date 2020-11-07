using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab = null;

    [SerializeField] private GridManager gridManager = null;

    void Start()
    {
        SpawnApple();
    }
    
    /* Instantiate an apple at a specified location on the grid */
    // 25 x 25 grid (x = -12, 12) y = (12, -12)
    public void SpawnApple()
    {
        Instantiate(applePrefab, new Vector3(Random.Range(-gridManager.cols / 2, gridManager.cols / 2), Random.Range(gridManager.rows / 2, -gridManager.rows / 2), 0), Quaternion.identity);
    }
}
