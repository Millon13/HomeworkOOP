using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class Enemy :MonoBehaviour
    {
       
        [Header("Enemy")] 
        [SerializeField] private ShipController enemy;//получаем кораблик и дальше им управляем из енеми
       //вместо наследования должно быть делигирование- делегат действие
        [SerializeField] private ShipController target;
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
       
        [SerializeField] private Dead dead;
        
        [Header("Movement")]
        private bool isNotReached;
        private Vector2 distance;
        private Vector2 distanceNormal;
        private Vector3 moveDirection;

    
      //  public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        //private void OnEnable() => dead.OnDead += this.OnCharacterDead;

        //private void OnDisable() => dead.OnDead -= this.OnCharacterDead;

     //   private void OnCharacterDead() => _despawner.Despawn(this);

        private void FixedUpdate()
        {
            
            
            SetNormal();
            enemy.Move(distanceNormal);
            TimeFire(time);

        }
        public void SetNormal()
        {
            distance = destination - (Vector2)this.transform.position;
            isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            moveDirection = isNotReached ? distance.normalized : Vector3.zero;
            distanceNormal = distance.normalized;
            
         
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