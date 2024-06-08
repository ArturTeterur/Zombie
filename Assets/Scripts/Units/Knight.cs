using System.Collections.Generic;
using Scripts.Level.HealthBar;
using Scripts.Units.CharacterMovements;
using Scripts.Units.Enemys;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Units.Knights
{
    public class Knight : CharacterMovement
    {
        public override Vector3 Target => _positionEnemy;

        [SerializeField] private Vector3 _positionEnemy;
        [SerializeField] private Enemy _targetEnemy;
        [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();
        [SerializeField] private GameObject _healthBarPrefab;
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private UnityEvent _onDied;
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;

        private HealthBar _healthBar;

        public event UnityAction Died
        {
            add => _onDied.AddListener(value);
            remove => _onDied.RemoveListener(value);
        }

        private void Start()
        {
            base.Start();
            _maxHealth = _health;
            GameObject healthBar = Instantiate(_healthBarPrefab);
            _healthBar = healthBar.GetComponent<HealthBar>();
            _healthBar.Setup(transform);
        }

        private void Update()
        {
            base.Update();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                _enemyList.Add(enemyComponent);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                _enemyList.Remove(enemyComponent);
            }
        }

        private void OnDestroy()
        {
            if (_healthBar)
            {
                Destroy(_healthBar.gameObject);
                _onDied.Invoke();
            }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _healthBar.SetHealth(_health, _maxHealth);

            if (_health <= 0)
            {
                Instantiate(_deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        public override void DoDamage(int damage)
        {
            _targetEnemy.TakeDamage(1);
        }

        public override void GetClosest()
        {
            float minDistance = Mathf.Infinity;
            Enemy closestEnemy = null;

            foreach (Enemy go in _enemyList)
            {
                if (go != null)
                {
                    float distance = Vector3.Distance(transform.position, go.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = go;
                    }
                }
            }

            if (closestEnemy != null)
            {
                _targetEnemy = closestEnemy;
                _positionEnemy = closestEnemy.transform.position;
            }
        }
    }
}

