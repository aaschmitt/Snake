using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnAppleEaten;
    
    [SerializeField] private float speed = 1f;

    private Direction _currentDirection;
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }


    void Start()
    {
        StartCoroutine(TimeMovement());
    }
    
    void Update()
    {
        HandleInput();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

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

    private IEnumerator TimeMovement()
    {
        while (true)
        {
            MovePlayer();
            yield return new WaitForSeconds(speed);
        }
    }

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

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player has left Game Area");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Apple")
        {
            Debug.Log("Snake collided with an apple!");
            Destroy(other.gameObject);
            OnAppleEaten.Invoke();
        }
    }
}
