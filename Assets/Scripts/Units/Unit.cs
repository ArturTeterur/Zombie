using System;
using System.Collections.Generic;
using Scripts.Level.HealthBar;
using Scripts.Units.State;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Units.CharacterMovements
{
    public abstract class Unit : MonoBehaviour
    {
        private const string StateRun = "Run";
        private const string StateAttack = "Attack";

        public event Action OnDestroyHealthBar;

        public List<Unit> UnitsList { get; set; }

        [SerializeField] private bool _itsShooter;
        [SerializeField] private States _currentState;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _distanceToAttack;
        [SerializeField] private float _attackPeriod;
        [SerializeField] private GameObject _flash;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private GameObject _healthBarPrefab;
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private bool itsEnemy;
        [SerializeField] private Vector3 _positionUnit;

        private int _damage = 1;
        private float _minimalAudioPitch = 0.8f;
        private float _maximalAudioPitch = 1.2f;
        private float _effectTime = 0.8f;
        private float _timer = 0f;
        private HealthBar _healthBar;

        protected Unit _targetUnit;
        protected Vector3 Target;

        public void Start()
        {
            SetState(States.Walk);
            _maxHealth = _health;
            GameObject healthBar = Instantiate(_healthBarPrefab);
            _healthBar = healthBar.GetComponent<HealthBar>();
            _healthBar.Setup(transform);
        }

        public void Update()
        {
            if (Target != null)
            {
                DetectingNearestUnit();
            }

            if (_currentState == States.Walk)
            {
                if (Target != Vector3.zero)
                {
                    _navMeshAgent.SetDestination(Target);
                    _animator.SetBool(StateRun, true);
                    float distance = Vector3.Distance(transform.position, Target);
                    if (distance <= _distanceToAttack)
                    {
                        SetState(States.Attack);
                    }

                    if (Target == null)
                    {
                        _currentState = States.Idle;
                    }
                }
            }
            else if (_currentState == States.Attack)
            {
                if (_itsShooter == true)
                {
                    transform.LookAt(Target);
                }

                _animator.SetTrigger(StateAttack);
                if (Target != Vector3.zero)
                {
                    _timer += Time.deltaTime;

                    if (_timer > _attackPeriod)
                    {
                        _timer = 0;
                        DoDamage(_damage);
                        _audioSource.pitch = Random.Range(_minimalAudioPitch, _maximalAudioPitch);
                        _audioSource.Play();
                        if (_itsShooter == true)
                        {
                            _flash.SetActive(true);
                            Invoke("HideFlash", _effectTime);
                        }
                    }
                }
            }
            else if (_currentState == States.Idle)
            {
                _animator.SetBool(StateRun, true);
            }
        }

        public abstract void DoDamage(int damage);

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _healthBar.SetHealth(_health, _maxHealth);

            if (_health <= 0)
            {
                Instantiate(_deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                OnDestroy();
                OnDestroyHealthBar?.Invoke();
            }
        }

        public void DetectingNearestUnit()
        {
            float minDistance = Mathf.Infinity;

            Unit ClosestUnit = null;

            foreach (Unit unit in UnitsList)
            {
                if (unit != null)
                {
                    float distance = Vector3.Distance(transform.position, unit.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        ClosestUnit = unit;
                    }
                }
            }

            if (ClosestUnit != null)
            {
                _targetUnit = ClosestUnit;
                _positionUnit = ClosestUnit.transform.position;
            }
        }

        private void SetState(States state)
        {
            _currentState = state;

            if (_currentState == States.Walk)
            {
                if (Target != Vector3.zero)
                {
                    DetectingNearestUnit();
                    _navMeshAgent.SetDestination(Target);
                }
            }
        }

        private void OnDestroy()
        {
            if (_healthBar)
            {
                Destroy(_healthBar.gameObject);
            }
        }

        private void HideFlash()
        {
            _flash.SetActive(false);
        }
    }
}