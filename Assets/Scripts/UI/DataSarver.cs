using UnityEngine;

namespace Scripts.UI.DataSarver
{
    public class DataSarver : MonoBehaviour
    {
        [SerializeField] private string _objectName;
        [SerializeField] private int _id;

        public void SaveInt()
        {
            PlayerPrefs.SetInt(_objectName, _id);
        }
    }
}