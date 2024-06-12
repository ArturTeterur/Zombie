using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _textLevel;
    [SerializeField] private GameObject _menuWin;
    [SerializeField] private GameObject _menulose;

    public void UnitsWin()
    {
        Time.timeScale = 0f;
        _textLevel.SetActive(false);
        _menuWin.SetActive(true);
    }

    public void EnemyWin()
    {
        Time.timeScale = 0f;
        _menulose.SetActive(true);
        _textLevel.SetActive(false);
    }
}
