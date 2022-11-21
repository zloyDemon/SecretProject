using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 20f;
    [Header("Jump")]
    [SerializeField] private float _jumpTime = 0.4f;
    [SerializeField] private float _jumpPower = 220f;
    [SerializeField] private float _fallMultiplier = 50f;
    [SerializeField] private float _jumpMultiplier = 30f;
    

    private Rigidbody2D _playerRigidBody;
    private Vector2 _input;
    private bool _isJumping;
    private Vector2 _gravityVector;
    private float _jumpCounter;
    private BoxCollider2D _boxCollider2D;
    
    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _gravityVector = new Vector2(0, -Physics2D.gravity.y);
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);
        Jump();
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

    }

    private void FlipX()
    {
        var xValue = _input.x;
        Vector2 characterLocalScale = transform.localScale;
        if (xValue > 0)
        {
            characterLocalScale.x = 1;
        }
        else if (xValue < 0)
        {
            characterLocalScale.x = -1;
        }

        transform.localScale = characterLocalScale;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        { 
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _jumpPower);
            _isJumping = true;
            _jumpCounter = 0;
        }
        
        if (_playerRigidBody.velocity.y > 0 && _isJumping)
        {
            _jumpCounter += Time.deltaTime;
            if (_jumpCounter > _jumpTime)
            {
                _isJumping = false;
            }

            float t = _jumpCounter / _jumpTime;
            float currentJumpMultiplier = _jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpMultiplier = _jumpMultiplier * (1 - t);
            }
            
            _playerRigidBody.velocity += _gravityVector * currentJumpMultiplier * Time.deltaTime;
        }
        
        if (_playerRigidBody.velocity.y < 0)
        {
            _playerRigidBody.velocity -= _gravityVector * (_fallMultiplier * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
            _jumpCounter = 0;

            if (_playerRigidBody.velocity.y > 0)
            {
                _playerRigidBody.velocity =
                    new Vector2(_playerRigidBody.velocity.x, _playerRigidBody.velocity.y * 0.8f);
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center + Vector3.up * 2, _boxCollider2D.bounds.size, .1f, Vector2.down, 5);
        return hit2D.collider != null;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 10, Vector3.one * 15);
    }
}
