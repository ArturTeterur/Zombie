using System.Collections.Generic;
using Scripts.UI.RollDice;
using Scripts.Units.Knights;
using UnityEngine;

namespace Scripts.Level.Spawner
{
    public class Spawner : MonoBehaviour
    {
        private const string Characters = "Characters";

        [SerializeField] private GameObject _textLevel;
        [SerializeField] private GameObject _menuWin;
        [SerializeField] private GameObject _menulose;
        [SerializeField] private RollDice _dice;
        [SerializeField] private List<Knight> _characters;
        [SerializeField] private List<Knight> _knightList = new List<Knight>();

        private int _onDestroy = 0;
        private int _numberCharters;
        private float _coordinateMinimalX = -3f;
        private float _coordinateMaximalX = 3f;
        private float _coordinateY = 0f;
        private float _coordinateMinimalZ = -9f;
        private float _coordinateMaximalZ = -11f;
        private int _charaterId;

        public void CreatCharacter()
        {
            _charaterId = PlayerPrefs.GetInt(Characters);
            _numberCharters = _dice.Number + 1;

            for (int i = 0; i < _numberCharters; i++)
            {
                Vector3 _position = new Vector3(Random.Range(_coordinateMinimalX, _coordinateMaximalX), _coordinateY, Random.Range(_coordinateMinimalZ, _coordinateMaximalZ));
                Knight newKnight = Instantiate(_characters[_charaterId], _position, Quaternion.identity);
                newKnight.Died += CountDestroyed;
                _knightList.Add(newKnight);
            }
        }

        private void OnDisable()
        {
            foreach (var Knight in _knightList)
                Knight.Died -= CountDestroyed;
        }

        private void CountDestroyed()
        {
            _onDestroy++;
            if (_numberCharters == _onDestroy)
            {
                EnemyWin();
            }
        }

        private void EnemyWin()
        {
            Time.timeScale = 0f;
            _menulose.SetActive(true);
            _textLevel.SetActive(false);
        }
    }
}