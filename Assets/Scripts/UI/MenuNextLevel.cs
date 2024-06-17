using Agava.YandexGames;
using Scripts.Level.Coins;
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
        [SerializeField] private Coins _coinDispley;
        [SerializeField] private Shop _shop;
        [SerializeField] private SoundMuteHandler _soundMuteHandler;

        private int _mainMenuNumber = 1;
        private int _nextLevelNumber;
        private int _rewardWinning = 100;
        private int _rewardLosing = 10;
        private int _currentCountCoinsPlayers = 0;
        private int _lastLevelNumber = 23;

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
            ExitLevel(LeaderboardName, _currentCountCoinsPlayers, _rewardLosing);
        }

        public void NextLevel()
        {
            _nextLevelNumber = SceneManager.GetActiveScene().buildIndex + 1;
            if (_nextLevelNumber == _lastLevelNumber)
            {
                _nextLevelNumber = SceneManager.GetActiveScene().buildIndex;
            }

            PlayerPrefs.SetInt(CurrentLevel, _nextLevelNumber);
            _shop.ReceivingAward(_rewardWinning);
            SceneManager.LoadScene(_nextLevelNumber);
            ExitLevel(LeaderboardName, _currentCountCoinsPlayers, _rewardWinning);
        }

        public void MenuWinExit()
        {
            ExitLevel(LeaderboardName, _currentCountCoinsPlayers, _rewardWinning);
            SceneManager.LoadScene(_mainMenuNumber);
        }

        public void MenuWinLose()
        {
            ExitLevel(LeaderboardName, _currentCountCoinsPlayers, _rewardLosing);
            SceneManager.LoadScene(_mainMenuNumber);
        }

        private void ExitLevel(string leaderBoardName, int currentCountCoins, int reward)
        {
            _shop.ReceivingAward(reward);
            if (PlayerAccount.IsAuthorized)
            {
                Leaderboard.SetScore(leaderBoardName, currentCountCoins += reward);
            }

            PlayerPrefs.Save();
        }
    }
}