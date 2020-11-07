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

    /* Private fields */
    private Direction _currentDirection;
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
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

        transform.position = new Vector2(x, y);
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

    /* If player collides with an apple, destroy it and spawn a new apple */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Apple")
        {
            Destroy(other.gameObject);
            OnAppleEaten.Invoke();
        }
    }
}
