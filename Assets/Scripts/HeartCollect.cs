using UnityEngine;

public class HeartCollect : MonoBehaviour
{
    [SerializeField] private string text;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.CollectHeart();
        GameManager.Instance.Player.ShowFlyText(text);
        gameObject.SetActive(false);
    }
}