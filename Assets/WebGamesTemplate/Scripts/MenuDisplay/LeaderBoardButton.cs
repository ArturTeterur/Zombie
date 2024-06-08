using System;
using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private LeaderBoardDisplay _leaderBoardDisplay;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _canvasAutorization;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        _closeButton.onClick.AddListener(OnCloseClock);
        _leaderBoardDisplay.SetLeaderboardScore();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _closeButton.onClick.RemoveListener(OnCloseClock);
    }


    private void OnCloseClock()
    {
        Hide();
    }

    private void OnClick()
    {
        if (_leaderBoardDisplay.gameObject.activeSelf)
            Hide();
        else
            Show();
    }

    private void Show()
    {
        if (PlayerAccount.IsAuthorized)
        {
            _leaderBoardDisplay.gameObject.SetActive(true);
            _leaderBoardDisplay.SetLeaderboardScore();
            _leaderBoardDisplay.OpenYandexLeaderboard();
        }
        if (!PlayerAccount.IsAuthorized)
        {
            _canvasAutorization.gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        _leaderBoardDisplay.gameObject.SetActive(false);
    }
}