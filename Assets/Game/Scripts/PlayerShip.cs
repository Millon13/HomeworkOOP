using Codice.Client.Common.GameUI;
using Modules.UI;
using Modules.Utils;
using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerShip :MonoBehaviour,  IShipMove
    {
        [SerializeField] PlayerInputSys playerInput;
        [SerializeField]
        private TransformBounds _playerArea;

        private Transform _playerTransform;
        public Transform _viewTransform;

        [SerializeField] private ShipControllerViewConfig _viewConfig;
        [SerializeField] private BulletFire _bulletFire;
        [SerializeField] private BulletSpawner _bulletspawner;
        [SerializeField]
        private CameraShaker _cameraShaker;
        
        public ShipControllerSO config;


        [Header("Health")]
        public int currentHealth;


        [Header("Movement")]

        private Vector3 moveDirection;
        [SerializeField]
        private Motor _motor;
        [SerializeField] private Animations _animations;

        [Header("UI")]//убрать отсюда
        [SerializeField]
        private GameOverView _gameOverView;//

        [SerializeField]//убрать отсюда
        private HealthView _healthView;

        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        //public event Action<BulletSpawner> OnFire;

        private void OnEnable()
        {
            this.OnHealthChanged += health =>
            {
                _healthView.SetHealth(health, this.config.Health);
                _cameraShaker.Shake();
            };
            this.OnDead += _gameOverView.Show;
        }

        private void OnDisable()
        {
            this.OnHealthChanged -= health =>
            {
                _healthView.SetHealth(health, this.config.Health);
                _cameraShaker.Shake();
            };
            this.OnDead -= _gameOverView.Show;// не должен заниматься сметрью персонажа
        }
        
        public void Update()
        {
        
            playerInput.Fire(this);
            playerInput.Move();
            Move();
        }

       
        
        private void LateUpdate()
        {
           
            AnimateMovement(_viewConfig);
            this.transform.position = _playerArea.ClampInBounds(this.transform.position);
        }
        public void Move()
        {    
            _motor.MoveInspect();
            this.moveDirection = new Vector2(playerInput.dx, playerInput.dy);
            if (this.currentHealth > 0)
            {
                _motor.MoveStep(this.moveDirection);
            }
        }

    
        private void AnimateMovement(ShipControllerViewConfig _viewConfig)
        {
            _animations.AnimateMovement(Time.deltaTime, moveDirection, _viewTransform, _viewConfig);
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
        
    }
   
}

