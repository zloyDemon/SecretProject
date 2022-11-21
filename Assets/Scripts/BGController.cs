using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    
    private void FixedUpdate()
    {
        transform.position = new Vector3(_mainCamera.position.x, _mainCamera.position.y, transform.position.z);
    }
}
