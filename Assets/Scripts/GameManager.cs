using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int MaxHeartCount = 2;
    
    [SerializeField] private UIGame _uiGame;
    [SerializeField] private PlayerController _player;
    
    public static GameManager Instance { get; private set; }
    
    public int HeartCount { get; private set; }
    public bool IsGamePause { get; private set; } = false;
    public PlayerController Player => _player;

    private bool _isInstructionReaded;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
            
        DontDestroyOnLoad(gameObject);
        Player.GetComponent<PlayerController>().enabled = false;
        _uiGame.ShowTutorial();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isInstructionReaded)
        {
            PauseGame(!IsGamePause);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void CollectHeart()
    {
        HeartCount++;
        _uiGame.UpdateHeartCountUI(HeartCount);
    }

    public void OnFinishEnter()
    {
        string message = "";
        
        if (HeartCount == MaxHeartCount)
        {
            message = "Win";
        }
        else
        {
            _player.ShowStaticText("Need more");
        }
        
        Debug.Log(message);
    }

    public void CompleteTutorial()
    {
        _uiGame.CompleteTutorial();
        _isInstructionReaded = true;
        Player.GetComponent<PlayerController>().enabled = true;
    }

    public void PauseGame(bool isPause)
    {
        IsGamePause = isPause;
        Time.timeScale = IsGamePause ? 0 : 1;
        _uiGame.SetPauseMenuActive(isPause);
    }
}
