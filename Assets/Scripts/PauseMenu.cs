using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _exitBtn;

    private void Awake()
    {
        _continueBtn.onClick.AddListener(OnContinueClick);
        _exitBtn.onClick.AddListener(OnExitClick);
    }

    private void OnContinueClick()
    {
        GameManager.Instance.PauseGame(false);
    }

    private void OnExitClick()
    {
        GameManager.Instance.ExitGame();
    }
}
