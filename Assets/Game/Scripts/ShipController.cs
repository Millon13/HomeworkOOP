using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    // +
    public abstract class ShipController :MonoBehaviour,IShipFire
    {
        public Transform firePoint;
        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        public event Action<ShipController> OnFire;
        public Transform _viewTransform;
        
        [SerializeField]
        private ShipControllerViewConfig _viewConfig;
        [SerializeField] private BulletFire _bulletFire;

        public ShipControllerSO config;
        
      
        [Header("Health")]
        public int currentHealth;
        protected virtual void FixedUpdate() => _motor.FixedUpdate();

        [Header("Movement")]
    
        protected Vector3 moveDirection;
        [SerializeField]
        protected Motor _motor;
        [SerializeField] protected Animations _animations;

       /* public void FireAction()
        {
            this.OnFire?.Invoke(this);

        }*/

        private void Awake()
        {
            this.currentHealth = config.Health;
           _motor.SetSpeed(config.MoveSpeed);

            _animations.AnimateAwake(_viewConfig);
            
        }

       
 
        
        protected void AnimateMovement(ShipControllerViewConfig _viewConfig)
        {
            _animations.AnimateMovement(Time.deltaTime, moveDirection, _viewTransform,_viewConfig);
        }
        protected virtual void LateUpdate()
        {
            AnimateMovement(_viewConfig);
        }



        public void NotifyAboutHealthChanged(int health)
        {
            if (health > 0)
                _animations.AnimateDamage(_viewConfig);

            this.OnHealthChanged?.Invoke(health);
        }

        public void NotifyAboutDead()
        {

            //_animations.VFXIntitiator(prefab,_viewConfig);
             ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
            Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

            this.OnDead?.Invoke();
        }
        public void Fire()
        {
            this.OnFire?.Invoke(this);
        }
        
     
    }

    
}