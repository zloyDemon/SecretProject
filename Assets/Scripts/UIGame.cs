using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _heartCountText;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private RectTransform _finishPopup;
    [SerializeField] private RectTransform _tutorialView;
    [SerializeField] private TextMeshProUGUI _infoText;

    private Coroutine _corShowText;
    
    private void Awake()
    {
        UpdateHeartCountUI(0);
    }

    public void UpdateHeartCountUI(int currentCount)
    {
        string text = $"{currentCount}/{GameManager.MaxHeartCount}";
        _heartCountText.text = text;
    }

    public void SetPauseMenuActive(bool isActive)
    {
        _pauseMenu.gameObject.SetActive(isActive);
    }

    public void ShowFinalPopup()
    {
        _finishPopup.gameObject.SetActive(true);
    }

    public void CompleteTutorial()
    {
        _tutorialView.gameObject.SetActive(false);
    }

    public void ShowTutorial()
    {
        _tutorialView.gameObject.SetActive(true);
    }

    public void ShowInfoText(string text)
    {
        if (_corShowText != null)
        {
            StopCoroutine(_corShowText);
            _corShowText = null;
        }

        _corShowText = StartCoroutine(CorShowText(text));
    }
    
    private IEnumerator CorShowText(string text)
    {
        float waitTime = text.Length * 0.25f;
        var waiter = new WaitForSeconds(waitTime);
        _infoText.text = text;
        yield return waiter;
        _infoText.text = string.Empty;
        _corShowText = null;
    }
}
