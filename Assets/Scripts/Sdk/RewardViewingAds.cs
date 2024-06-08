using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Sdk.RewardViewingAds
{
    public class RewardViewingAds : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonShowAdv;
        [SerializeField] private Shop _shop;
        [SerializeField] private SoundMuteHandler _soundMuteHandler;
        [SerializeField] private GameObject _soundButton;
        [SerializeField] private Button _buttonAd;

        private int _rewardViewingAds = 50;

        public void ShowAdvButton()
        {
            Agava.YandexGames.VideoAd.Show(OnOpenVideo, OnRewarded, OnClose, OnError);
        }

        private void OnError(string obj)
        {
            _soundMuteHandler.OnVideoOpened();
            Time.timeScale = 0;

            if (obj != null)
            {
                _soundMuteHandler.OnVideoOpened();
            }
        }

        private void OnOpenVideo()
        {
            Time.timeScale = 0;
            _soundMuteHandler.OnVideoOpened();
            _buttonAd.interactable = false;
            _buttonShowAdv.SetActive(false);
        }

        private void OnRewarded()
        {
            Time.timeScale = 0;
            _shop.ReceivingAward(_rewardViewingAds);
        }

        private void OnClose()
        {
            Time.timeScale = 1;
            _soundMuteHandler.OnVideoClosed();
        }
    }
}