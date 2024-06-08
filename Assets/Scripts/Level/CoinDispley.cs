using UnityEngine;
using TMPro;

namespace Scripts.Level.CoinDispley
{
    public class CoinDispley : MonoBehaviour
    {
        private const string SaveNumberOfCoin = "_saveNumberOfCoin";

        [SerializeField] private TextMeshProUGUI _textCoins;
        [SerializeField] private GameObject _buttonShowAdv;
        [SerializeField] private int _numberOfCoins;

        private int _saveNumberOfCoin;

        public int NumberOfCoins => _numberOfCoins;

        private void Start()
        {
            if (PlayerPrefs.HasKey(SaveNumberOfCoin))
            {
                _numberOfCoins = PlayerPrefs.GetInt(SaveNumberOfCoin);
            }

            UpdateCoins();
            transform.parent = null;
        }

        public void UpdateCoins()
        {
            _textCoins.text = _numberOfCoins.ToString();
        }
    }
}