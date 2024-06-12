using Scripts.Level.DispleyCoins;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const string Characters = "Characters";
    private const string SaveNumberOfCoin = "_saveNumberOfCoin";
    private const string CharacterGirl = "numberGirl";
    private const string CharacterBoy = "numberBoy";
    private const string CharacterPolice = "numberPolice";
    private const string CharacterSoldier = "numberSoldier";
    private const string CharacterHero = "numberHero";

    [SerializeField] private DispleyCoins _coinDispley;
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private GameObject _buttonChartersGirl;
    [SerializeField] private GameObject _buttonChartersBoy;
    [SerializeField] private GameObject _buttonChartersPolice;
    [SerializeField] private GameObject _buttonChartersSolder;
    [SerializeField] private GameObject _buttonChartersHero; 
    [SerializeField] private int _currentCountCoinsPlayers = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(SaveNumberOfCoin))
        {
            _currentCountCoinsPlayers = PlayerPrefs.GetInt(SaveNumberOfCoin);
        }
    }

    private void Start()
    {
        RemoveButton(CharacterGirl, _buttonChartersGirl);
        RemoveButton(CharacterBoy, _buttonChartersBoy);
        RemoveButton(CharacterPolice, _buttonChartersPolice);
        RemoveButton(CharacterSoldier, _buttonChartersSolder);
        RemoveButton(CharacterHero, _buttonChartersHero);
    }

    public void BuyUnit(int priceUnit, GameObject buttonUnit, int numberCharters)
    {
        if (_currentCountCoinsPlayers >= priceUnit)
        {
            BuyUnit(priceUnit, buttonUnit, numberCharters);
            _currentCountCoinsPlayers -= priceUnit;
            PlayerPrefs.SetInt(name, numberCharters);
            PlayerPrefs.SetInt(Characters, numberCharters);
            Destroy(buttonUnit);
            _currentCountCoinsPlayers = PlayerPrefs.GetInt(SaveNumberOfCoin);
            PlayerPrefs.SetInt(SaveNumberOfCoin, _currentCountCoinsPlayers);
            PlayerPrefs.Save();
            _textCoins.text = _currentCountCoinsPlayers.ToString();
        }
    }

    public void ReceivingAward(int reward)
    {
        _currentCountCoinsPlayers += reward;
        PlayerPrefs.SetInt(SaveNumberOfCoin, _currentCountCoinsPlayers);
        _textCoins.text = _currentCountCoinsPlayers.ToString();
        PlayerPrefs.Save();
    }

    private void RemoveButton(string buttonName, GameObject button)
    {
        if (PlayerPrefs.HasKey(buttonName))
        {
            Destroy(button);
        }
    }
}
