using System;
using UnityEngine;

namespace CommandPattern
{
    public class GameController : MonoBehaviour
    {
        private GameControls _controls;
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject reticle;
        
        [SerializeField] private float force;

        public int aimModifier = 1;
        
        private Vector3 _targetDir;
        private Vector3 _shootingPoint;

        private Command _reverseAim;

        private void OnEnable()
        {
            _controls.Game.Enable();
        }
        
        private void OnDisable()
        {
            _controls.Game.Disable();
        }

        private void Awake()
        {
            _controls = new GameControls();
            _reverseAim = new ReverseAim(this);
            
            _controls.Game.LeftClick.performed += _ => Shoot();
            _controls.Game.Space.performed += _ => _reverseAim.Execute();
            _controls.Game.Aiming.performed += ctx => Aim(ctx.ReadValue<Vector2>());
        }

        private void Aim(Vector2 location)
        {
            location *= aimModifier;
            _targetDir = new Vector3(location.x, location.y, 0);
            reticle.transform.position = _targetDir;
        }

        private void Shoot()
        {
            GameObject obj = Instantiate(bulletPrefab, _shootingPoint, bulletPrefab.transform.rotation);

            Rigidbody projectileRb = obj.GetComponent<Rigidbody>();
            projectileRb.AddForce(force * Vector3.forward, ForceMode.Impulse);
        }
    }
}