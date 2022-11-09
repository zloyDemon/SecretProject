using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 20f;

    private Rigidbody2D _playerRigidBody;
    
    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        Move(new Vector2(inputX, 0));
    }

    private void Move(Vector2 direction)
    {
        Vector2 raycastStart = transform.position;
        Vector2 raycastDirection = Vector2.down;
        float distance = 1.5f;
        var hit = Physics2D.Raycast(raycastStart, raycastDirection, distance);

        Vector2 normal = hit.normal;

        Vector2 project = direction - Vector2.Dot(direction, normal) * normal;
        Vector2 moveOffset = project * (_moveSpeed * Time.deltaTime);
        
        _playerRigidBody.velocity = new Vector2(moveOffset.x, _playerRigidBody.velocity.y);
        
        Debug.DrawRay(raycastStart, raycastDirection * distance, Color.cyan);
        Debug.DrawRay(raycastStart, project * distance, Color.green);
        Debug.DrawRay(hit.point, hit.normal * distance, Color.red);
    }
}
