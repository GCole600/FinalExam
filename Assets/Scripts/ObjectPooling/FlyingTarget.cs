using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace ObjectPooling
{
    public class FlyingTarget : MonoBehaviour
    {
        public IObjectPool<FlyingTarget> Pool { get; set; }
        
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float _lerpProgress;
        
        public float lerpTime = 10f;
        
        private void OnDisable()
        {
            ResetPosition();
        }
        
        private void Update()
        {
            // Move the target from start pos to target pos over set time
            _lerpProgress += Time.deltaTime / lerpTime;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _lerpProgress);

            // Return to pool when it reaches the target pos
            if (_lerpProgress >= 1f)
                ReturnToPool();
        }

        public void SetTargetPosition()
        {
            _startPosition = transform.position;
            _targetPosition = new Vector3(Random.Range(-11, 11), 15, 10);
        }

        private void ResetPosition()
        {
            transform.position = _startPosition;
            _lerpProgress = 0f;
        }
        
        public void ReturnToPool()
        {
            Pool.Release(this);
        }
    }
}
