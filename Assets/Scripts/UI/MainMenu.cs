using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private const string CurrentLevel = "_currentLevel";

        private int _numberNextLevel;

        public void NextLevel()
        {
            _numberNextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(_numberNextLevel);
        }

        public void StartLevel()
        {
            if (PlayerPrefs.HasKey(CurrentLevel))
            {
                int level = PlayerPrefs.GetInt(CurrentLevel);
                SceneManager.LoadScene(level);
            }
            else
            {
                _numberNextLevel = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(_numberNextLevel);
            }
        }
    }
}