using System.Collections.Generic;
using Scripts.Units.CharacterMovements;
using Scripts.Units.Enemys;
using UnityEngine;

namespace Scripts.Units.Knights
{
    public class Knight : Unit
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

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                UnitsList.Add(enemyComponent);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                UnitsList.Remove(enemyComponent);
            }
        }

        public override void DoDamage(int damage)
        {
            _targetUnit.TakeDamage(_damage);
        }
    }
}