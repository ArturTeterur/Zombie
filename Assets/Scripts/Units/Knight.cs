using System;
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
        [SerializeField] private Vector3 _positionEnemy;
        [SerializeField] private Enemy _targetEnemy;
        [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();

        private int _damage = 1;

        protected new Vector3 Target => _positionEnemy;

        private new void Start()
        {
            base.Start();
        }

        private new void Update()
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

        public override void DoDamage(int damage)
        {
            _targetEnemy.TakeDamage(_damage);
        }

        public override void DetectingNearestEnemy()
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