using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    /* Public fields */
    public int rows = 25;
    public int cols = 25;
    
    /* Serialized private fields */
    [SerializeField] private float tileSize = 1;

    /* Private fields */
    private BoxCollider2D _boxCollider2D;
    
    
    /* Get Components and Generate the grid*/
    void Start()
    {
        GetComponents();
        GenerateGrid();
    }

    /* Generates and Centers grid, resizes and centers BoxCollider2D */
    private void GenerateGrid()
    {
        /* Generate Grid */
        GameObject referenceTile = (GameObject) Instantiate(Resources.Load("GameTile"));        // Load the GameTile prefab from the Resources folder
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject) Instantiate(referenceTile, transform);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }
        
        Destroy(referenceTile);                                                                       // Destroy the referenceTile created at beginning of method

        /* Center Grid */
        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        
        /* Resize & Center BoxCollider2D */
        _boxCollider2D.size = new Vector2(gridW, gridH);
        _boxCollider2D.offset = new Vector2(gridW / 2 + tileSize / 2 - 1, -gridH / 2 - tileSize / 2 + 1);
    }

    /* Get references to any other needed components attached to this gameobject */
    private void GetComponents()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
