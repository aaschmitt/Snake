﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    /* Public fields */
    public UnityEvent OnAppleEaten;
    public UnityEvent OnSnakeDeath;
    
    /* Serialized private fields */
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject snakeBodyPrefab = null;
    [SerializeField] private Transform snakeBodyParent = null;
    [SerializeField] private ParticleSystem snakeDeathPS = null;
    [SerializeField] private ParticleSystem appleEatPS = null;

    /* Private fields */
    private int _size = 0;                    // size of 0 means just head
    private List<Vector3> _snakePositionList;
    private List<GameObject> _snakeParts;
    private Direction _currentDirection;
    private bool _keyPressed;
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

    /* Stop any coroutines and spawn particle system when object is destroyed */
    void OnDestroy()
    {
        OnSnakeDeath.Invoke();
        Instantiate(snakeDeathPS, transform);
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
            _keyPressed = false;
            yield return new WaitForSeconds(speed);
        }
    }

    /* Change _currentDirection depending upon input (WASD) */
    private void HandleInput()
    {
        if (_keyPressed) return;
        
        if (Input.GetKeyDown(KeyCode.A) && _currentDirection != Direction.Right)                         // Move left
        {
            _currentDirection = Direction.Left;
            _keyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _currentDirection != Direction.Left)                    // Move right
        {
            _currentDirection = Direction.Right;
            _keyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _currentDirection != Direction.Up)                    // Move down
        {
            _currentDirection = Direction.Down;
            _keyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && _currentDirection != Direction.Down)                    // Move up
        { 
            _currentDirection = Direction.Up;
            _keyPressed = true;
        }
    }

    /* Destroy the player if they exit the game area (hit the wall) */
    private void OnTriggerExit2D(Collider2D other)
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this);
    }

    /* If player collides with an apple, destroy it and spawn a new apple. If collides with snakeBody, destroy snake */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Apple")
        {
            Destroy(other.gameObject);
            OnAppleEaten.Invoke();
            Instantiate(appleEatPS, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            ExtendSnake();
        }

        if (other.gameObject.tag == "SnakeBody")
        {
            Destroy(this);
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
