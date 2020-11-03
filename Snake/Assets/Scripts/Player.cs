using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Instance variables
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationWhenUp = 0.0f;
    [SerializeField] private float rotationWhenDown = 180f;
    [SerializeField] private float rotationWhenLeft = 90f;
    [SerializeField] private float rotationWhenRight = -90f;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    private Direction _currentDirection;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _currentDirection = Direction.Up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _currentDirection = Direction.Left;
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _currentDirection = Direction.Down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _currentDirection = Direction.Right;
        }

        RotateImage(_currentDirection);
    }

    private void RotateImage(Direction direction)
    {
        float rotation = 0f;
        switch (direction)
        {
            case (Direction.Up):
                rotation = rotationWhenUp;
                break;
            case (Direction.Down):
                rotation = rotationWhenDown;
                break;
            case (Direction.Left):
                rotation = rotationWhenLeft;
                break;
            case (Direction.Right):
                rotation = rotationWhenRight;
                break;
        }
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    private void MovePlayer()
    {
        switch (_currentDirection)
        {
            case (Direction.Up):
                transform.position += new Vector3(0, Time.deltaTime * speed, 0);
                break;
            case (Direction.Down):
                transform.position += new Vector3(0, -1 * Time.deltaTime * speed, 0);
                break;
            case (Direction.Left):
                transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);
                break;
            case (Direction.Right):
                transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
                break;
        }
    }
}
