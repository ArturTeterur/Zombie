using Lean.Localization;
using UnityEngine;

namespace Scripts.UI.Language
{
    public class Language : MonoBehaviour
    {
        private const string RussianLanguage = "Russian";
        private const string TurkishLanguage = "Turkish";
        private const string EnglishLanguage = "Russian";
        private const string CurrentLanguage = "_currentLanguage";
        private const string RussianLanguageDesignation = "ru";
        private const string TurkishLanguageDesignation = "tr";
        private const string EnglishLanguageDesignation = "en";


        [SerializeField] private LeanLocalization _leanLocalization;

        private string _language;

        private void Start()
        {
            LoadLocalization();
        }

        private void LoadLocalization()
        {
            _language = PlayerPrefs.GetString(CurrentLanguage);
            switch (_language)
            {
                case RussianLanguageDesignation:
                    _leanLocalization.SetCurrentLanguage(RussianLanguage);
                    break;
                case TurkishLanguageDesignation:
                    _leanLocalization.SetCurrentLanguage(TurkishLanguage);
                    break;
                case EnglishLanguageDesignation:
                    _leanLocalization.SetCurrentLanguage(EnglishLanguage);
                    break;
                default:
                    _leanLocalization.SetCurrentLanguage(RussianLanguage);
                    break;
            }
        }
    }
}