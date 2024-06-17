using System;
using System.Collections.Generic;
using Scripts.Level.HealthBar;
using Scripts.Units.CharacterMovements;
using Scripts.Units.Knights;
using UnityEngine;

namespace Scripts.Units.Enemys
{
    public class Enemy : CharacterMovement
    {
        [SerializeField] private Vector3 _positionKnight;
        [SerializeField] private Knight _targetKnight;
        [SerializeField] private List<Knight> _knight;

        private int _damage = 1;

        protected new Vector3 Target => _positionKnight;

        private new void Start()
        {
            base.Start();
        }

        private new void Update()
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

        public override void DoDamage(int damage)
        {
            _targetKnight.TakeDamage(_damage);
        }

        public override void DetectingNearestEnemy()
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