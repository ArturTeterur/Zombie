using Scripts.Units.State;
using UnityEngine;

namespace Scripts.Units.CharacterMovements
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        private const string StateRun = "Run";
        private const string StateAttack = "Attack";

        [SerializeField] private bool _itsShooter;
        [SerializeField] private States _currentState;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _distanceToAttack;
        [SerializeField] private float _attackPeriod;
        [SerializeField] private GameObject _flash;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;

        private int _damage = 1;
        private float _minimalAudioPitch = 0.8f;
        private float _maximalAudioPitch = 1.2f;
        private float _effectTime = 0.8f;
        private float _timer = 0f;

        public Vector3 Target { get; } = Vector3.zero;

        public void Start()
        {
            SetState(States.Walk);
        }

        public void Update()
        {
            if (Target != null)
            {
                GetNearestEnemy();
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

        public void SetState(States state)
        {
            _currentState = state;

            if (_currentState == States.Walk)
            {
                if (Target != Vector3.zero)
                {
                    GetNearestEnemy();
                    _navMeshAgent.SetDestination(Target);
                }
            }
        }

        public void HideFlash()
        {
            _flash.SetActive(false);
        }

        public abstract void GetNearestEnemy();

        public abstract void DoDamage(int damage);
    }
}