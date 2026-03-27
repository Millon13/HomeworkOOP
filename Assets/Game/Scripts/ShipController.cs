using System;
using Codice.Client.Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    // +
    public class ShipController :MonoBehaviour
    {
        [SerializeField] private Motor motor;
        [SerializeField] private Health _health;
        [SerializeField] private Dead _dead;
        [SerializeField] private BulletFire bulletFire;
        [SerializeField] private BulletSpawner spawner;
        [SerializeField] public TeamType teamType=TeamType.None;

        
        public void Update()
        {
            _health.OnHealthChanged += SubstactHealth;
            _dead.OnDead += ShipDeath;
        }
        public void SubstactHealth(int health)
        {
          _health.currentHealth -=health;
            
        }
        public void ShipDeath()

        {

            if (_health.currentHealth < 0)
                Destroy(gameObject);
        }

        public void Move(Vector3 moveDirection)
        {
            motor.MoveInspect();

            if (_health.currentHealth > 0)
            {
                motor.MoveStep(moveDirection);
            }
        }
        private void FixedUpdate()
        {

            

            
            //TimeFire(time);

        }
        public void SetEnemyMovement(Vector2 distance, float _stoppingDistance, Vector2 destination,
            ShipController target, Vector3 moveDirection, bool isNotReached)
        {
            motor.MoveInspect();
            motor.SetSpeed(motor._speed);
            if (_health.currentHealth <= 0 || target._health == null || target._health.currentHealth <= 0)
                return;
            distance = destination - (Vector2)this.transform.position;
            isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            moveDirection = isNotReached ? distance.normalized : Vector3.zero;

            if (isNotReached)
            {
                motor.MoveStep(distance.normalized);
            }

        }

        // public Transform firePoint;
        //public event Action<int> OnHealthChanged;
        // public event Action OnDead;
        // public event Action<BulletSpawner> OnFire;
        //  public Transform _viewTransform;

        //[SerializeField]
        // protected ShipControllerViewConfig _viewConfig;
        // [SerializeField] private BulletFire _bulletFire;
        // [SerializeField] private BulletSpawner _bulletspawner;

        //  public ShipControllerSO config;


        //[Header("Health")]
        // public int currentHealth;


        //[Header("Movement")]

        // protected Vector3 moveDirection;
        // [SerializeField]
        //protected Motor _motor;
        // [SerializeField] protected Animations _animations;


        /* private void Awake()
         {
             this.currentHealth = config.Health;
            _motor.SetSpeed(config.MoveSpeed);

             _animations.AnimateAwake(_viewConfig);

         }*/



        /*
        protected void AnimateMovement(ShipControllerViewConfig _viewConfig)
        {
            _animations.AnimateMovement(Time.deltaTime, moveDirection, _viewTransform,_viewConfig);
        }
        



        public void NotifyAboutHealthChanged(int health)
        {
            if (health > 0)
                _animations.AnimateDamage(_viewConfig);

            this.OnHealthChanged?.Invoke(health);
        }

        public void NotifyAboutDead()
        {

            
            ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
            Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

            this.OnDead?.Invoke();
        }
        public void Fire(ShipController shipController)
        {
            this.OnFire?.Invoke(_bulletspawner);
        }*/


    }

    
}