using UnityEngine;

namespace Scripts.Units.State
{
    public class UnitState : MonoBehaviour
    {
        public enum States
        {
            Idle,
            Walk,
            Attack,
            Die,
        }
    }
}