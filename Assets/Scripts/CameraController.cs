using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MinLevelEdge = -32f;
    private const float MaxLevelEdge = 385;
    
    [SerializeField] private Transform _target;
    [SerializeField] private SpriteRenderer _bg;

    private void Update()
    {
        Vector3 followVector = (Vector2)_target.position;

        followVector.y = Mathf.Clamp(followVector.y, 0, 125);
        followVector.x = Mathf.Clamp(followVector.x, MinLevelEdge, MaxLevelEdge);
        followVector.z = transform.position.z;
        
        transform.position = followVector;

        Vector3 cameraPosition = transform.position;
        cameraPosition.z = _bg.transform.position.z;
        _bg.transform.position = cameraPosition;
    }
}
