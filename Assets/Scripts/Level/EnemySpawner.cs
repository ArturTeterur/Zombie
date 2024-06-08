using System.Collections.Generic;
using Scripts.Units.Enemys;
using UnityEngine;

namespace Scripts.Level.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _textLevel;
        [SerializeField] private GameObject _menuWin;
        [SerializeField] private GameObject _menulose;
        [SerializeField] private int _numberEnemy;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private List<Enemy> EnemyList = new List<Enemy>();

        private float _coordinateMinimalX = -3f;
        private float _coordinateMaximalX = 3f;
        private float _coordinateY = 0f;
        private float _coordinateMinimalZ = -2f;
        private float _coordinateMaximalZ = 2f;
        private float _coordinateRotationX = 0f;
        private float _coordinateRotationY = 180f;
        private float _coordinateRotationZ = 0f;
        private int _onDestroy;

        private void Start()
        {
            _onDestroy = _numberEnemy;
        }

        public void CreatEnemy()
        {
            for (int i = 0; i < _numberEnemy; i++)
            {
                Vector3 _position = new Vector3(Random.Range(_coordinateMinimalX, _coordinateMaximalX), _coordinateY, Random.Range(_coordinateMaximalZ, _coordinateMinimalZ));
                transform.rotation = Quaternion.Euler(_coordinateRotationX, _coordinateRotationY, _coordinateRotationZ) * transform.rotation;
                Enemy newEnemy = Instantiate(_enemyPrefab, _position, transform.rotation);
                newEnemy.DiedEnemy += CountDestroyed;
                EnemyList.Add(newEnemy);
            }
        }

        private void CountDestroyed()
        {
            _onDestroy--;

            if (_onDestroy == 0)
            {
                UnitsWin();
            }
        }

        private void OnDisable()
        {
            foreach (var enemy in EnemyList)
                enemy.DiedEnemy -= CountDestroyed;
        }

        private void UnitsWin()
        {
            Time.timeScale = 0f;
            _textLevel.SetActive(false);
            _menuWin.SetActive(true);
        }
    }
}