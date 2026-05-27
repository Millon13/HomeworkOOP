using UnityEngine;

namespace Game
{
    // +
    [RequireComponent(typeof(FireComponent), typeof(MotorComponent), typeof(HealthComponent))]
    public class Ship : MonoBehaviour
    {
        [SerializeField] private MotorComponent _motorComponent;

        [SerializeField] private HealthComponent _healthComponent;

        [SerializeField] private FireComponent _fireComponent;

        [SerializeField] public TeamType teamType = TeamType.None;

        private bool CanMove;

        private bool CanFire;

        private void Awake()
        {
            _fireComponent = this.GetComponent<FireComponent>();
            _motorComponent = this.GetComponent<MotorComponent>();
            _healthComponent = this.GetComponent<HealthComponent>();
            _healthComponent.isAlive = true;
        }

        public void Update()
        {
            _fireComponent.CanFire = _healthComponent.isAlive;
            _motorComponent.MoveEnabled = _healthComponent.isAlive;
        }


        public void Move(Vector3 moveDirection)
        {
            if (_motorComponent.MoveEnabled)
            {
                _motorComponent.SetSpeed(_motorComponent._speed);
                _motorComponent.MoveStep(moveDirection);
            }
        }
    }
}