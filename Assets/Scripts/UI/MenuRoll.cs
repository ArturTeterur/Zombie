using UnityEngine;

namespace Scripts.UI.MenuRoll
{
    public class MenuRoll : MonoBehaviour
    {
        [SerializeField] private GameObject _menuButton;
        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _rollButton;
        [SerializeField] private GameObject _levelText;

        private void Start()
        {
            OpenMenuRoll();
        }

        public void OpenMenuRoll()
        {
            _menuButton.SetActive(true);
            _menuWindow.SetActive(true);
            Time.timeScale = 0f;
        }

        public void CloseMenuRoll()
        {
            _menuButton.SetActive(false);
            _menuWindow.SetActive(false);
            Time.timeScale = 1f;
        }

        public void PlayButton()
        {
            _rollButton.SetActive(true);
            _menuButton.SetActive(false);
        }

        public void OpenLevelText()
        {
            _levelText.SetActive(true);
        }
    }
}