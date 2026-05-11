using System;
using Codice.Client.Common;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    // +
    [RequireComponent(typeof(Fire),typeof(Motor),typeof(Health))]
    public class ShipController :MonoBehaviour
    {
        
        [SerializeField] private Motor _motor;

        [SerializeField] private Health _health;
    
        [SerializeField] private Fire _fire;
 
        [SerializeField] public TeamType teamType=TeamType.None;

        private bool CanMove;
        private bool CanFire;
        private void Awake()
        {
            _fire = this.GetComponent<Fire>();
            _motor = this.GetComponent<Motor>();
            _health = this.GetComponent<Health>();
            _health.isAlive = true;
        }
        public void Update()
        {
            _fire.CanFire = _health.isAlive;
            _motor.CanMove = _health.isAlive;
            //_health.OnHealthChanged += OnSubstactHealth;
        }
        public void OnSubstactHealth(int health)
        {
          _health.currentHealth -=health;  
        }
        public void ShipDeath()

        {
            if (_health.currentHealth < 0)
                Destroy(gameObject);
        }
      
        public void Move(Vector3 moveDirection)
        {
            if (_motor.CanMove)
            {
                _motor.MoveInspect();
                _motor.SetSpeed(_motor._speed);
                _motor.MoveStep(moveDirection);
            }
            else
                return;
        }

        public Vector2 GetFireDirection()
        {
            Vector2 fireDirection =Vector2.zero;
            if (teamType == TeamType.Enemy)
            {
                fireDirection =Vector2.down;
            }
            if (teamType == TeamType.Player)
            {
                fireDirection =Vector2.up;
            }
            return fireDirection;
        }

       
    }

    
}