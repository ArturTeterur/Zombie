using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Sdk.Init
{
    public class Init : MonoBehaviour
    {
        private int _menuSceneNumber = 1;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            yield return Agava.YandexGames.YandexGamesSdk.Initialize(OnInitialized);
        }

        private void OnInitialized()
        {
            PlayerPrefs.SetString("_currentLanguage", YandexGamesSdk.Environment.i18n.lang);
            SceneManager.LoadScene(_menuSceneNumber);
        }
    }
}