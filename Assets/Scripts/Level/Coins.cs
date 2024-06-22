using TMPro;
using UnityEngine;

namespace Scripts.Level.Coins
{
    public class Coins : MonoBehaviour
    {
        private const string SaveNumberOfCoin = "_saveNumberOfCoin";

        [SerializeField] private TextMeshProUGUI _textCoins;
        [SerializeField] private int _numberOfCoins;

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