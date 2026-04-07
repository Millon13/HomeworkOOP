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
        private ShipController _target;
        public Vector2 destination;
       
        [SerializeField] private BulletSpawner _bulletspawner;
        [SerializeField]
        private float _fireCooldown = 1.25f;
        [SerializeField] Fire fire;
       
        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private float _fireTime;
        private float time;

        
        public IEnemyDespawner _despawner;
       
       // [SerializeField] private Dead dead;
        
        [Header("Movement")]
        private bool isNotReached;
        private Vector2 distance;
        private Vector2 distanceNormal;
        private Vector3 moveDirection;
        private float speed = 1.5f;
        private int damage = 1;
    
        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;




        private void OnCharacterDead()
        {
            if (this == null || gameObject == null)
                return;

            Debug.Log($"Enemy {gameObject.name} died");
            Health health = GetComponent<Health>();
            if (health != null)
                health.OnDead -= OnCharacterDead;

            if (_despawner == null)
            {
                _despawner = FindObjectOfType<MonoBehaviour>() as IEnemyDespawner;

                var spawner = FindObjectOfType<EnemySpawner>();
                if (spawner != null && spawner is IEnemyDespawner)
                {
                    _despawner = spawner as IEnemyDespawner;
                }

                if (_despawner == null)
                {
                    Debug.LogError($"No IEnemyDespawner found in scene for {gameObject.name}");
                    Destroy(gameObject);
                    return;
                }
            }

            _despawner.Despawn(this);
        }
        

        public void Awake()
        {
            PlayerInputSys player = FindObjectOfType<PlayerInputSys>();
            _target = player.GetComponent<ShipController>();

        }
        public void Update()
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

                if (!isNotReached && _target != null)
                {
                    
                      time = Time.time;
                    if (time - _fireTime >= _fireCooldown)
                    {
                        Vector2 position = fire._firePoint.position;
                        Vector2 target = _target.transform.position;
                        Vector2 direction = (target - position).normalized;

                        fire.FireTo(position, direction);
                        _fireTime = time;
                    }
                }


          
        }
        private void OnEnable()
        {
            Health health = GetComponent<Health>();
            if (health != null)
                health.OnDead += this.OnCharacterDead;
        }


        private void OnDisable()
        {
            // Отписываемся при деактивации
            Health health = GetComponent<Health>();
            if (health != null)
                health.OnDead -= OnCharacterDead;
        }

        public void Despawn(Enemy enemy)
        {
            throw new NotImplementedException();
        }
    }

}