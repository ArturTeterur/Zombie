using System.Collections.Generic;
using Scripts.Level.HealthBar;
using Scripts.Units.CharacterMovements;
using Scripts.Units.Knights;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Units.Enemys
{
    public class Enemy : CharacterMovement
    {
        public override Vector3 Target => _positionKnight;

        [SerializeField] private Vector3 _positionKnight;
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private Knight _targetKnight;
        [SerializeField] private GameObject _healthBarPrefab;
        [SerializeField] private UnityEvent _onDied;
        [SerializeField] private List<Knight> _knight;
        [SerializeField] private GameObject _deathEffect;

        private HealthBar _healthBar;

        public event UnityAction DiedEnemy
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

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<Knight>(out Knight unitComponent))
            {
                _knight.Add(unitComponent);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<Knight>(out Knight unitComponent))
            {
                _knight.Remove(unitComponent);
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
            _targetKnight.TakeDamage(1);
        }

        public override void GetClosest()
        {
            float minDistance = Mathf.Infinity;
            Knight closestUnit = null;

            foreach (Knight go in _knight)
            {
                if (go != null)
                {
                    float distance = Vector3.Distance(transform.position, go.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestUnit = go;
                    }
                }
            }

            if (closestUnit != null)
            {
                _targetKnight = closestUnit;
                _positionKnight = closestUnit.transform.position;
            }
        }
    }
}