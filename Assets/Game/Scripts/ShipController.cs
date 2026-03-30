using System;
using Codice.Client.Common;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    // +
    [RequireComponent(typeof(Fire),typeof(Motor),typeof(Health))]
    public class ShipController :MonoBehaviour
    {
        
        [SerializeField] private Motor _motor;
        [SerializeField] private Health _health;
        //[SerializeField] private Dead _dead;
        [SerializeField] private Fire _fire;
        //[SerializeField] private BulletSpawner spawner;
        [SerializeField] public TeamType teamType=TeamType.None;

        private bool CanMove;
        private bool CanFire;
        private void Awake()
        {
            _fire = this.GetComponent<Fire>();
            _motor = this.GetComponent<Motor>();
            _health = this.GetComponent<Health>();
            _health.isAlive = true;
        }
        public void Update()
        {
            _fire.CanFire = _health.isAlive;
            _motor.CanMove = _health.isAlive;
          

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
            if (_motor.CanMove)
            {
                _motor.MoveInspect();
                _motor.SetSpeed(_motor._speed);
                _motor.MoveStep(moveDirection);
            }
           
        }

        public Vector2 GetFireDirection()
        {
            Vector2 fireDirection =Vector2.zero;
            if (teamType == TeamType.Enemy)
            {
                fireDirection =Vector2.down;
            }
            if (teamType == TeamType.Player)
            {
                fireDirection =Vector2.up;
            }
            return fireDirection;
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