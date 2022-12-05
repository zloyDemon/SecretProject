using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.OnFinishEnter();
    }
}
