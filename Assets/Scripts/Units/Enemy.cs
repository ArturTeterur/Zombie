using System.Collections.Generic;
using Scripts.Units.CharacterMovements;
using Scripts.Units.Knights;
using UnityEngine;

namespace Scripts.Units.Enemys
{
    public class Enemy : Unit
    {
        private int _damage = 1;

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
                UnitsList.Add(unitComponent);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<Knight>(out Knight unitComponent))
            {
                UnitsList.Remove(unitComponent);
            }
        }

        public override void DoDamage(int damage)
        {
            _targetUnit.TakeDamage(_damage);
        }
    }
}