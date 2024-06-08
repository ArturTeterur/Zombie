using Lean.Localization;
using UnityEngine;

namespace Scripts.UI.Language
{
    public class Language : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        private string _language;

        private void Start()
        {
            LoadLocalization();
        }

        private void LoadLocalization()
        {
            _language = PlayerPrefs.GetString("_currentLanguage");
            switch (_language)
            {
                case "ru":
                    _leanLocalization.SetCurrentLanguage("Russian");
                    break;
                case "tr":
                    _leanLocalization.SetCurrentLanguage("Turkish");
                    break;
                case "en":
                    _leanLocalization.SetCurrentLanguage("English");
                    break;
                default:
                    _leanLocalization.SetCurrentLanguage("Russian");
                    break;
            }
        }
    }
}