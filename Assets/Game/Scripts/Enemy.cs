using UnityEngine;

namespace Game
{
    // +
    public sealed class Enemy : ShipController
    {
       
        [Header("Enemy")]
        public ShipController target;
        public Vector2 destination;

        [SerializeField]
        private float _fireCooldown = 1.25f;

        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private float _fireTime;
        private float time;

        private IEnemyDespawner _despawner;

        private bool isNotReached;
        private Vector2 distance;




      // [SerializeField] private BulletFire _enemyBulletFire;

        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        private void OnEnable() => this.OnDead += this.OnCharacterDead;

        private void OnDisable() => this.OnDead -= this.OnCharacterDead;

        private void OnCharacterDead() => _despawner.Despawn(this);

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _motor.SetSpeed(config.MoveSpeed);
            if (this.currentHealth <= 0 || this.target == null || this.target.currentHealth <= 0)
                return;
            Vector2 distance = destination - (Vector2) this.transform.position;     
            bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            moveDirection = isNotReached ? distance.normalized : Vector3.zero;

            if (isNotReached)
            {
                _motor.MoveStep(distance.normalized);
            }
            else
            {

                TimeFire(time);
            }

        }
        public void TimeFire(float time)
        {

            time = Time.time;
            if (time - _fireTime >= _fireCooldown)
            {
                 Fire();
                _fireTime = time;
            }
        }
        



    }
}