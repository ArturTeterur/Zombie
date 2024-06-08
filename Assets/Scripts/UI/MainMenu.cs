using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private const string CurrentLevel = "_currentLevel";

        private int _nextLevel;

        public void NextLevel()
        {
            _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(_nextLevel);
        }

        public void MainMenuLevel()
        {
            if (PlayerPrefs.HasKey(CurrentLevel))
            {
                int level = PlayerPrefs.GetInt(CurrentLevel);
                SceneManager.LoadScene(level);
            }
            else
            {
                _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(_nextLevel);
            }
        }
    }
}