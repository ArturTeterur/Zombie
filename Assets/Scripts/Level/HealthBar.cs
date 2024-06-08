using UnityEngine;

namespace Scripts.Level.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform _scaleTransform;
        [SerializeField] private Transform _target;

        private Transform _cameraTransform;
        private float _yScale = 1f;
        private float _zScale = 1f;
        private float _heightHealthBar = 2f;
        private float _xScale;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.position = _target.position + Vector3.up * _heightHealthBar;
            transform.rotation = _cameraTransform.rotation;
        }

        public void Setup(Transform target)
        {
            _target = target;
        }

        public void SetHealth(int health, int maxHealth)
        {
            _xScale = (float)health / maxHealth;
            _xScale = Mathf.Clamp01(_xScale);
            _scaleTransform.localScale = new Vector3(_xScale, _yScale, _zScale);
        }
    }
}