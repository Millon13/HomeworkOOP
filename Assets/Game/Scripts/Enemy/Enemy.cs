using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class Enemy :MonoBehaviour
    {
        [SerializeField] private ShipController shipController;//получаем кораблик и дальше им управляем из енеми
       //вместо наследования должно быть делигирование- делегат действие
        [Header("Enemy")]
        public Transform firePoint;
        public ShipController target;
        public Vector2 destination;

        [SerializeField] private BulletSpawner _bulletspawner;
        [SerializeField]
        private float _fireCooldown = 1.25f;
        [SerializeField] Fire fire;
        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private float _fireTime;
        private float time;

        //private IEnemyDespawner _despawner;

        private bool isNotReached;
        private Vector2 distance;

       
        [SerializeField] private Dead dead;
        
        [Header("Movement")]

        private Vector3 moveDirection;
        [SerializeField] private Motor _motor;
        public ShipControllerSO config;
        [Header("Health")]
        public int currentHealth;
      //  public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        //private void OnEnable() => dead.OnDead += this.OnCharacterDead;

        //private void OnDisable() => dead.OnDead -= this.OnCharacterDead;

     //   private void OnCharacterDead() => _despawner.Despawn(this);

        private void FixedUpdate()
        {
            
            shipController.SetEnemyMovement(distance,_stoppingDistance,destination,target,moveDirection,isNotReached);
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