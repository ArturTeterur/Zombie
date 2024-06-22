using System.Collections.Generic;
using Scripts.UI.RollDice;
using Scripts.Units.Enemys;
using Scripts.Units.Knights;
using UnityEngine;

public class SpawnUnits : MonoBehaviour
{
    private const string Characters = "Characters";

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Knight> _characters;
    [SerializeField] private List<Knight> _knightList = new List<Knight>();
    [SerializeField] private List<Enemy> EnemyList = new List<Enemy>();
    [SerializeField] private RollDice _dice;
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private bool _itsEnemy;
    [SerializeField] private float _coordinateMinimalX;
    [SerializeField] private float _coordinateMaximalX;
    [SerializeField] private float _coordinateY;
    [SerializeField] private float _coordinateMinimalZ;
    [SerializeField] private float _coordinateMaximalZ;
    [SerializeField] private float _coordinateRotationX;
    [SerializeField] private float _coordinateRotationY;
    [SerializeField] private float _coordinateRotationZ;

    private int _charaterId;
    private int _numberEnemy;
    private int _numberUnits;
    private int _numberCharters;
    private int _onDestroyEnemys;
    private int _onDestroyKnight;

    private void Start()
    {
        _onDestroyEnemys = _numberEnemy;
    }

    private void OnDisable()
    {
        foreach (var knight in _knightList)
            knight.OnDestroyHealthBar -= OnCountDestroyedKnights;

        foreach (var knight in _knightList)
            knight.OnDestroyHealthBar -= OnCountDestroyedKnights;
    }

    public void CreatUnits()
    {
        if (!_itsEnemy)
        {
            _charaterId = PlayerPrefs.GetInt(Characters);
            _numberCharters = _dice.Number + 1;
            _numberUnits = _numberCharters;
        }
        else
        {
            _numberUnits = _numberEnemy;
        }

        for (int i = 0; i < _numberUnits; i++)
        {
            float randomCoordinateX = Random.Range(_coordinateMinimalX, _coordinateMaximalX);
            float randomCoordinateZ = Random.Range(_coordinateMinimalZ, _coordinateMaximalZ);

            Vector3 position = new Vector3(randomCoordinateX, _coordinateY, randomCoordinateZ);
            if (!_itsEnemy)
            {
                Knight newKnight = Instantiate(_characters[_charaterId], position, Quaternion.identity);
                newKnight.OnDestroyHealthBar += OnCountDestroyedKnights;
                _knightList.Add(newKnight);
            }
            else
            {
                transform.rotation = Quaternion.Euler(_coordinateRotationX, _coordinateRotationY, _coordinateRotationZ) * transform.rotation;
                Enemy newEnemy = Instantiate(_enemyPrefab, position, transform.rotation);
                newEnemy.OnDestroyHealthBar += OnCountDestroyedEnemys;
                EnemyList.Add(newEnemy);
            }
        }
    }

    private void OnCountDestroyedEnemys()
    {
        _onDestroyEnemys--;

        if (_onDestroyEnemys == 0)
        {
            _gameOver.UnitsWin();
        }
    }

    private void OnCountDestroyedKnights()
    {
        _onDestroyKnight++;
        if (_numberCharters == _onDestroyKnight)
        {
            _gameOver.EnemyWin();
        }
    }
}
