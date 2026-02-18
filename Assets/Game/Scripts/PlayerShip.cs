using Codice.Client.Common.GameUI;
using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerShip : ShipController
    {
        [SerializeField] PlayerInputSys playerInput;
        [SerializeField]
        private TransformBounds _playerArea;

       // private Transform _playerTransform;

        [SerializeField]
        private CameraShaker _cameraShaker;

        [Header("UI")]
        [SerializeField]
        private GameOverView _gameOverView;

        [SerializeField]
        private HealthView _healthView;

        
        

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
            this.OnDead -= _gameOverView.Show;
        }
        
        public void Update()
        {
            playerInput.InputFire(this);
            playerInput.InputMovement();
            this.moveDirection = new Vector2(playerInput.dx, playerInput.dy);
            if (this.currentHealth > 0)
            {
                _motor.MoveStep(this.moveDirection);
            }
        }

       

        protected override void LateUpdate()
        {
            base.LateUpdate();
            this.transform.position = _playerArea.ClampInBounds(this.transform.position);
        }
    }
   
}

