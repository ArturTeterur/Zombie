using UnityEngine;

namespace Scripts.Sdk.InterstitialAd
{
    public class InterstitialAd : MonoBehaviour
    {
        [SerializeField] private SoundMuteHandler _soundMuteHandler;

        private void Awake()
        {
            ShowAdv();
        }

        private void OnEnable()
        {
            Application.focusChanged += OnApplicationFocusChanged;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnApplicationFocusChanged;
        }

        public void ShowAdv()
        {
            Agava.YandexGames.InterstitialAd.Show(Open, Close);
        }

        private void Close(bool close)
        {
            if (close)
            {
                _soundMuteHandler.OnVideoClosed();
            }
        }

        private void Open()
        {
            _soundMuteHandler.OnVideoOpened();
        }

        private void OnApplicationFocusChanged(bool focus)
        {
            if (focus == true)
            {
                _soundMuteHandler.OnVideoOpened();
            }
        }
    }
}