using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;

    private void LateUpdate()
    {
        transform.position = new Vector3(_playerTarget.position.x, _playerTarget.position.y, transform.position.z);
    }
}