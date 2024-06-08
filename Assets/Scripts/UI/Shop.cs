using Scripts.Level.CoinDispley;
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

    [SerializeField] private CoinDispley _coinDispley;
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private GameObject _buttonChartersGirl;
    [SerializeField] private GameObject _buttonChartersBoy;
    [SerializeField] private GameObject _buttonChartersPolice;
    [SerializeField] private GameObject _buttonChartersSolder;
    [SerializeField] private GameObject _buttonChartersHero;

    private int _priceGirl = 170;
    private int _priceBoy = 240;
    private int _pricePolice = 350;
    private int _priceSolder= 500;
    private int _priceHero = 700;
    private int _currentCountCoinsPlayers = 0;
    private int _numberGirl = 1;
    private int _numberBoy = 2;
    private int _numberPolice = 3;
    private int _numberSoldier = 4;
    private int _numberHero = 5;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(SaveNumberOfCoin))
        {
            _currentCountCoinsPlayers = PlayerPrefs.GetInt(SaveNumberOfCoin);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(CharacterGirl))
        {
            Destroy(_buttonChartersGirl);
        }

        if (PlayerPrefs.HasKey(CharacterBoy))
        {
            Destroy(_buttonChartersBoy);
        }

        if (PlayerPrefs.HasKey(CharacterPolice))
        {
            Destroy(_buttonChartersPolice);
        }

        if (PlayerPrefs.HasKey(CharacterSoldier))
        {
            Destroy(_buttonChartersSolder);
        }

        if (PlayerPrefs.HasKey(CharacterHero))
        {
            Destroy(_buttonChartersHero);
        }
    }

    public void BueUnit(int PriceUnit, GameObject ButtonUnit, int numberCharters)
    {
        if (_currentCountCoinsPlayers >= PriceUnit)
        {
            _currentCountCoinsPlayers -= PriceUnit;
            PlayerPrefs.SetInt(SaveNumberOfCoin, _currentCountCoinsPlayers);
            PlayerPrefs.SetInt(name, numberCharters);
            PlayerPrefs.SetInt(Characters, numberCharters);
            Destroy(ButtonUnit);
            _currentCountCoinsPlayers = PlayerPrefs.GetInt(SaveNumberOfCoin);
            PlayerPrefs.SetInt(SaveNumberOfCoin, _currentCountCoinsPlayers);
            PlayerPrefs.Save();
            _textCoins.text = _currentCountCoinsPlayers.ToString();
        }
    }

    public void BueGirl()
    {
        if (_currentCountCoinsPlayers >= _priceGirl)
        {
             BueUnit(_priceGirl, _buttonChartersGirl, _numberGirl);
             PlayerPrefs.SetInt(CharacterGirl, _numberGirl);
             Destroy(_buttonChartersGirl);
             PlayerPrefs.Save();
        }    
    }

    public void BueBoy()
    {
        if (_currentCountCoinsPlayers >= _priceBoy)
        {
            BueUnit(_priceBoy, _buttonChartersBoy, _numberBoy);
            PlayerPrefs.SetInt(CharacterBoy, _numberBoy);
            Destroy(_buttonChartersBoy);
            PlayerPrefs.Save();
        }
    }

    public void BuePolice()
    {
        if (_currentCountCoinsPlayers >= _pricePolice)
        {
            BueUnit(_pricePolice, _buttonChartersPolice, _numberPolice);
            PlayerPrefs.SetInt(CharacterPolice, _numberPolice);
            Destroy(_buttonChartersPolice);
            PlayerPrefs.Save();
        }
    }

    public void BueSolder()
    {
        if (_currentCountCoinsPlayers >= _priceSolder)
        {
            BueUnit(_priceSolder, _buttonChartersSolder, _numberSoldier);
            PlayerPrefs.SetInt(CharacterSoldier, _numberSoldier);
            Destroy(_buttonChartersSolder);
            PlayerPrefs.Save();
        }
    }

    public void BueHero()
    {
        if (_currentCountCoinsPlayers >= _priceHero)
        {
            BueUnit(_priceHero, _buttonChartersHero, _numberHero);
            PlayerPrefs.SetInt(CharacterHero, _numberHero);
            Destroy(_buttonChartersHero);
            PlayerPrefs.Save();
        }
    }
    
    public void ReceivingAward(int reward)
    {
        _currentCountCoinsPlayers += reward;
        PlayerPrefs.SetInt(SaveNumberOfCoin, _currentCountCoinsPlayers);
        _textCoins.text = _currentCountCoinsPlayers.ToString();
        PlayerPrefs.Save();
    }
}
