using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    // +
    public abstract class ShipController :MonoBehaviour
    {
      
        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        public Transform _viewTransform;
        public event Action<ShipController> OnFire;

        public ShipControllerSO config;
        private ParticleSystem prefab;
        [Header("Health")]
        public int currentHealth;
        protected virtual void FixedUpdate() => _motor.FixedUpdate();

        [Header("Movement")]
    
        protected Vector3 moveDirection;
        [SerializeField]
        protected Motor _motor;
        public Animations _animations;

        public void FireAction()
        {
            this.OnFire?.Invoke(this);
           
        }

        private void Awake()
        {
            if (config == null)
            {
                Debug.LogError($"Config is null в Awake! Имя объекта: {gameObject.name}", this);
                return;
            }
            this.currentHealth = config.Health;
           _motor.SetSpeed(config.MoveSpeed);

            _animations.AnimateAwake();
            
        }

        private void Update()
        {
            _motor.MoveInspect();

        }
 
        
        protected virtual void LateUpdate()
        {
            _animations.AnimateMovement(Time.deltaTime, moveDirection, _viewTransform);
        }

      
        
        public void NotifyAboutHealthChanged(int health)
        {
            if (health > 0)
                _animations.AnimateDamage();

            this.OnHealthChanged?.Invoke(health);
        }

        public void NotifyAboutDead()
        {
           
            _animations.VFXIntitiator(prefab);
            Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

            this.OnDead?.Invoke();
        }

        
    }/*public abstract class ShipController : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDead;

        public event Action<ShipController> OnFire;

        public ShipControllerSO config;

        [Header("Health")]
        public int currentHealth;

        [Header("Combat")]
        public Transform firePoint;
        public float bulletSpeed;
        public int bulletDamage;
        private float _fireTime;

        [Header("Movement")]
        [SerializeField]
        protected Motor _motor;
        
        protected Vector3 moveDirection;

        [Header("Visual")]
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private Transform _viewTransform;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ShipControllerViewConfig _viewConfig;

        [SerializeField]
        private ParticleSystem _fireVFX;

        [SerializeField]
        private AudioClip _fireSFX;

        [SerializeField]
        private AudioClip _damageSFX;

        private Material _material;
        private Tweener _damageAnimation;


        private void Awake()
        {
            this.currentHealth = config.Health;
            _motor.SetSpeed(config.MoveSpeed);

            _material = new Material(_viewConfig.MaterialPrefab);
            _renderer.material = _material;
        }

        protected virtual void FixedUpdate() => _motor.FixedUpdate();

        protected void Fire()
        {
            float time = Time.time;
            if (time - _fireTime < config.FireCooldown || this.currentHealth <= 0)
                return;

            if (_fireSFX)
                _audioSource.PlayOneShot(_fireSFX);

            if (_fireVFX)
                _fireVFX.Play();

            this.OnFire?.Invoke(this);
            _fireTime = time;
        }
        
        protected virtual void LateUpdate()
        {
            this.AnimateMovement(Time.deltaTime);
        }

        private void AnimateMovement(float deltaTime)
        {
            Vector3 shipAngles = _viewTransform.localEulerAngles;
            shipAngles.x = _viewConfig.MoveRotationAngle * moveDirection.y;
            shipAngles.y = _viewConfig.MoveRotationAngle / 2 * moveDirection.x * -1f;
            
            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = _viewConfig.MoveSpeed * deltaTime;
            _viewTransform.localRotation = Quaternion.Lerp(_viewTransform.localRotation, shipRotation, t);
        }
        
        public void NotifyAboutHealthChanged(int health)
        {
            if (health > 0)
                this.AnimateDamage();

            this.OnHealthChanged?.Invoke(health);
        }

        public void NotifyAboutDead()
        {
            // Instantiate particle vfx 
            ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
            Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

            this.OnDead?.Invoke();
        }

        private void AnimateDamage()
        {
            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();

            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                _viewConfig.HitDuration,
                progress => _material?.SetFloat(_viewConfig.HitPropertyName,
                    _viewConfig.HitAnimationCurve.Evaluate(progress))
            ).SetLink(_renderer.gameObject);

            if (_damageSFX)
                _audioSource.PlayOneShot(_damageSFX);
        }
    }*/
}