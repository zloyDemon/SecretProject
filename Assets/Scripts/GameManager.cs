using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public const int MaxHeartCount = 5;
    
    [SerializeField] private UIGame _uiGame;
    [SerializeField] private PlayerController _player;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _effectAudioSource;
    [Header("Musics")]
    [SerializeField] private AudioClip _bgMusic;
    [SerializeField] private AudioClip _hbMusic;

    [Header("Sounds Effect")] 
    [SerializeField] private AudioClip[] _jumpSounds;
    [SerializeField] private AudioClip[] _collectItemSounds;
    
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
        
        Player.GetComponent<PlayerController>().enabled = false;
        _uiGame.ShowTutorial();
        _musicAudioSource.clip = _bgMusic;
        _musicAudioSource.Play();
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
        PlayCollectSound();
    }

    public void OnFinishEnter()
    {
        if (HeartCount == MaxHeartCount)
        {
            _player.ResetCharacterMove();
            Player.enabled = false;
            _uiGame.ShowFinalPopup();
            _musicAudioSource.clip = _hbMusic;
            _musicAudioSource.Play();
        }
        else
        {
            _uiGame.ShowInfoText("Я собрала еще не все сердечки.");
        }
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

    public void PlayJumpSound()
    {
        var clip = _jumpSounds[Random.Range(0, _jumpSounds.Length)];
        _effectAudioSource.clip = clip;
        _effectAudioSource.PlayOneShot(clip);
    }

    public void PlayCollectSound()
    {
        var clip = _collectItemSounds[Random.Range(0, _collectItemSounds.Length)];
        _effectAudioSource.clip = clip;
        _effectAudioSource.PlayOneShot(clip);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
