using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float _jumpForce = 2000f;

    private Rigidbody2D _playerRigidBody;
    private Vector2 _input;
    private bool _isJumpPressed;
    
    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Move(_input);
    }

    private void Move(Vector2 direction)
    {
        Vector2 moveOffset = GetDirection(direction) * (_moveSpeed * 100 * Time.deltaTime);
        float moveValue = _input.x * _moveSpeed * 100 * Time.deltaTime;
        _playerRigidBody.velocity = new Vector2(moveValue, _playerRigidBody.velocity.y);
        FlipX();
        if (_isJumpPressed)
        {
            Jump();
            _isJumpPressed = false;
        }
        
    }

    private void FlipX()
    {
        var xValue = _input.x;
        Vector2 characterLocalScale = transform.localScale;
        if (xValue > 0)
        {
            characterLocalScale.x = 1;
        }
        else if(xValue < 0)
        {
            characterLocalScale.x = -1;
        }

        transform.localScale = characterLocalScale;
    }

    private void Jump()
    {
        _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _jumpForce);
    }

    private Vector2 GetDirection(Vector2 direction)
    {
        Vector2 raycastStart = transform.position;
        Vector2 raycastDirection = Vector2.down;
        float distance = 1.5f;
        var hit = Physics2D.Raycast(raycastStart, raycastDirection, distance);

        Vector2 normal = hit.normal;

        Vector2 project = direction - Vector2.Dot(direction, normal) * normal;
        return project;
    }
}
