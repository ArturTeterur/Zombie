using Agava.YandexGames;
using UnityEngine;

namespace Scripts.UI.AutorizationWindow
{
    public class AutorizationWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _autorizationWindow;

        public void CloseWindowAutorization()
        {
            _autorizationWindow.gameObject.SetActive(false);
        }

        public void Autorization()
        {
            PlayerAccount.Authorize();
        }
    }
}