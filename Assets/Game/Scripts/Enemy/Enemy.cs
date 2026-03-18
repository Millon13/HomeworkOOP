using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class Enemy :MonoBehaviour, IShipController//нарушает принцип барбары лисков
    {
       //вместо наследования должно быть делигирование- делегат действие
        [Header("Enemy")]
        public Transform firePoint;
        public PlayerShip target;
        public Vector2 destination;

        [SerializeField] private BulletSpawner _bulletspawner;
        [SerializeField]
        private float _fireCooldown = 1.25f;
        [SerializeField] Fire fire;
        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private float _fireTime;
        private float time;

        private IEnemyDespawner _despawner;

        private bool isNotReached;
        private Vector2 distance;

        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        public event Action<BulletSpawner> OnFire;
        [Header("Movement")]

        protected Vector3 moveDirection;
        [SerializeField] protected Motor _motor;
        public ShipControllerSO config;
        [Header("Health")]
        public int currentHealth;
        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        private void OnEnable() => this.OnDead += this.OnCharacterDead;

        private void OnDisable() => this.OnDead -= this.OnCharacterDead;

        private void OnCharacterDead() => _despawner.Despawn(this);

        protected void FixedUpdate()
        {
            
            _motor.MoveInspect();
            _motor.SetSpeed(config.MoveSpeed);
            if (this.currentHealth <= 0 || this.target == null || this.target.currentHealth <= 0)
                return;

            SetMovement();
            TimeFire(time);

        }
        public void SetMovement()
        {
            distance = destination - (Vector2)this.transform.position;
            isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            moveDirection = isNotReached ? distance.normalized : Vector3.zero;

            if (isNotReached)
            {
                _motor.MoveStep(distance.normalized);
            }
         
        }
      
        public void TimeFire(float time)
        {
            if (!isNotReached)
            { 
                time = Time.time;
                if (time - _fireTime >= _fireCooldown)
                {
                    fire.DoFire();
                    _fireTime = time;
                }

            }
          
        }
        



    }
}