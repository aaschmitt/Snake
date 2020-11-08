using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    /* Public fields */
    public UnityEvent OnAppleEaten;
    
    /* Serialized private fields */
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject snakeBodyPrefab = null;
    [SerializeField] private Transform snakeBodyParent = null;

    /* Private fields */
    private int _size = 0;                    // size of 0 means just head
    private List<Vector3> _snakePositionList;
    private List<GameObject> _snakeParts;
    private Direction _currentDirection;
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }


    /* Initialize necessary fields */
    void Awake()
    {
        _snakePositionList = new List<Vector3>();
        _snakeParts = new List<GameObject>();
    }
    
    /* Start player movement coroutine */
    void Start()
    {
        StartCoroutine(TimeMovement());
    }
    
    /* Check for Input each frame */
    void Update()
    {
        HandleInput();
    }

    /* Stop any coroutines when object is destroyed */
    void OnDestroy()
    {
        StopAllCoroutines();
    }

    /* Move the player one unit depending upon _currentDirection */
    private void MovePlayer()
    {
        int x = (int) transform.position.x;
        int y = (int) transform.position.y;
        
        _snakePositionList.Insert(0, new Vector3(x, y, 0));

        if (_snakePositionList.Count > _size + 1)
        {
            _snakePositionList.RemoveAt(_snakePositionList.Count - 1);
        }

        switch (_currentDirection)
        {
            case (Direction.Down):
                y--;
                break;
            case (Direction.Up):
                y++;
                break;
            case (Direction.Left):
                x--;
                break;
            case (Direction.Right):
                x++;
                break;
        }

        transform.position = new Vector2(x, y);                // Move snake head
        
        /* Move rest of snake body */
        for (int i = 0; i < _snakeParts.Count; i++)
        {
            _snakeParts[i].transform.position = _snakePositionList[i];
        }

    }

    /* Move player according to speed */
    private IEnumerator TimeMovement()
    {
        while (true)
        {
            MovePlayer();
            yield return new WaitForSeconds(speed);
        }
    }

    /* Change _currentDirection depending upon input (WASD) */
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentDirection = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _currentDirection = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _currentDirection = Direction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            _currentDirection = Direction.Up;
        }
    }

    /* Destroy the player if they exit the game area (hit the wall) */
    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    /* If player collides with an apple, destroy it and spawn a new apple. If collides with snakeBody, destroy snake */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Apple")
        {
            Destroy(other.gameObject);
            OnAppleEaten.Invoke();
            ExtendSnake();
        }

        if (other.gameObject.tag == "SnakeBody")
        {
            Destroy(this.gameObject);
        }
    }

    /* Instantiate more instances of snakeBody, making snake longer */
    private void ExtendSnake()
    {
        _size++;
        GameObject newSnakePart = Instantiate(snakeBodyPrefab, snakeBodyParent);
        newSnakePart.transform.position = _snakePositionList[_snakePositionList.Count - 1];
        _snakeParts.Insert(_snakeParts.Count, newSnakePart);
    }
}
