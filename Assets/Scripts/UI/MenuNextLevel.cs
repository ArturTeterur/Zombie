using Agava.YandexGames;
using Scripts.Level.CoinDispley;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI.MenuNextLevel
{
    public class MenuNextLevel : MonoBehaviour
    {
        private const string SaveNumberOfCoin = "_saveNumberOfCoin";
        private const string CurrentLevel = "_currentLevel";
        private const string LeaderboardName = "Coins";

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private CoinDispley _coinDispley;
        [SerializeField] private Shop _shop;
        [SerializeField] private SoundMuteHandler _soundMuteHandler;

        private int _menuSceneNumber = 1;
        private int _nextLevelNumber;
        private int _rewardWinning = 100;
        private int _rewardLosing = 10;
        private int _currentCountCoinsPlayers = 0;

        private void Start()
        {
            if (PlayerPrefs.HasKey(SaveNumberOfCoin))
            {
                _currentCountCoinsPlayers = PlayerPrefs.GetInt(SaveNumberOfCoin);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt(CurrentLevel, SceneManager.GetActiveScene().buildIndex);
            _shop.ReceivingAward(_rewardLosing);
            if (PlayerAccount.IsAuthorized)
            {
                Leaderboard.SetScore(LeaderboardName, _currentCountCoinsPlayers += _rewardLosing);
            }
            PlayerPrefs.Save();
        }

        public void NextLevel()
        {
            _nextLevelNumber = SceneManager.GetActiveScene().buildIndex + 1;
            if (_nextLevelNumber == 23)
            {
                _nextLevelNumber = SceneManager.GetActiveScene().buildIndex;
            }

            PlayerPrefs.SetInt(CurrentLevel, _nextLevelNumber);
            _shop.ReceivingAward(_rewardWinning);
            SceneManager.LoadScene(_nextLevelNumber);
            if (PlayerAccount.IsAuthorized)
            {
                Leaderboard.SetScore(LeaderboardName, _currentCountCoinsPlayers += _rewardWinning);
            }

            PlayerPrefs.Save();
        }

        public void MenuWinExit()
        {

            _shop.ReceivingAward(_rewardWinning);
            if (PlayerAccount.IsAuthorized)
            {
                Leaderboard.SetScore(LeaderboardName, _currentCountCoinsPlayers += _rewardWinning);
            }

            PlayerPrefs.Save();
            SceneManager.LoadScene(_menuSceneNumber);
        }

        public void MenuWinLose()
        {
            _shop.ReceivingAward(_rewardLosing);
            if (PlayerAccount.IsAuthorized)
            {
                Leaderboard.SetScore(LeaderboardName, _currentCountCoinsPlayers += _rewardLosing);
            }
            SceneManager.LoadScene(_menuSceneNumber);
            PlayerPrefs.Save();
        }
    }
}