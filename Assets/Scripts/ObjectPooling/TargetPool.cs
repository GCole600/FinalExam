using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace ObjectPooling
{
    public class TargetPool : MonoBehaviour
    {
        public GameObject targetPrefab;
        public float spawnRate;
        
        public int maxPoolSize = 10;
        public int stackDefaultCapacity = 10;
        
        private Vector3 _spawnPosition;
        
        private IObjectPool<FlyingTarget> _pool;

        private IObjectPool<FlyingTarget> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<FlyingTarget>(CreatedPooledItem, OnTakeFromPool, OnReturnedToPool, 
                        OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                return _pool;
            }
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnTarget), 0, spawnRate);
        }

        private void SpawnTarget()
        {
            var target = Pool.Get();

            // Randomize Spawn Pos
            _spawnPosition = new Vector3(Random.Range(-11, 11), 0, 0);
            target.transform.position = _spawnPosition;
            
            target.GetComponent<FlyingTarget>().SetTargetPosition();
        }
        
        private FlyingTarget CreatedPooledItem()
        {
            // Instantiate Target at randomized spawn pos
            GameObject target = Instantiate(targetPrefab, new Vector3(Random.Range(-11, 11), 0, 0), Quaternion.identity);

            FlyingTarget mover = target.GetComponent<FlyingTarget>();
            
            mover.Pool = Pool;
            
            return mover;
        }

        private void OnReturnedToPool(FlyingTarget target)
        {
            target.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(FlyingTarget target)
        {
            target.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(FlyingTarget target)
        {
            Destroy(target.gameObject);
        }
    }
}
