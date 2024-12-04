using ObjectPooling;
using UnityEngine;

namespace ObserverPattern
{
    public class Bullet : Subject
    {
        private void OnEnable()
        {
            AddObserver(FindObjectOfType<UiManager>());
        }
        
        private void OnDisable()
        {
            RemoveObserver(FindObjectOfType<UiManager>());
        }

        private void Start()
        {
            Destroy(gameObject, 3);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                other.GetComponent<FlyingTarget>().ReturnToPool();
                NotifyObservers();
            }
        }
    }
}
