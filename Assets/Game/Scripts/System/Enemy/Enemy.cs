using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class Enemy : MonoBehaviour
    {
        [Header("Enemy")] [SerializeField] private Ship enemy;
        private GameObject _target;
        public Vector2 destination;


        [SerializeField] private float _fireCooldown = 1.25f;
        [SerializeField] Fire fire;

        [SerializeField] private float _stoppingDistance = 0f;

        private float _fireTime;
        private float time;
        private BulletConfig config;
        private BulletViewConfig viewConfig;

        public IEnemyDespawner _despawner;


        [Header("Movement")] private bool isNotReached;
        private Vector2 distance;
        private Vector2 distanceNormal;
        private Vector3 moveDirection;

        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        private void OnEnable()
        {
            Health health = GetComponent<Health>();
            if (health != null)
                health.OnDead += this.OnCharacterDead;
        }


        private void OnDisable()
        {
            Health health = GetComponent<Health>();
            if (health != null)
                health.OnDead -= OnCharacterDead;
        }


        private void OnCharacterDead()
        {
            if (this == null || gameObject == null)
                return;

            Debug.Log($"Enemy {gameObject.name} died");
            Health health = GetComponent<Health>();
            if (health != null)
                health.OnDead -= OnCharacterDead;

            if (_despawner != null)
            {
                _despawner.Despawn(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void Update()
        {
            SetNormal();
            enemy.Move(moveDirection);
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

                    fire.FireTo(config, viewConfig, direction);
                    _fireTime = time;
                }
            }
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
    }
}