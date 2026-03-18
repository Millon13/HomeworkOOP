using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    // +
    public interface IShipController 
    {
       // public Transform firePoint;
        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        public event Action<BulletSpawner> OnFire;
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