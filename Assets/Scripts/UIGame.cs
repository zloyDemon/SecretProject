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
}
