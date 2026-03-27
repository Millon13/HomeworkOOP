using UnityEngine;
using Modules.Utils;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Game;
using PlasticGui;

public class Bullet:MonoBehaviour
{
    [SerializeField] public TeamType Team = TeamType.None;
    public Vector2 Direction;
    
    [SerializeField] public int Damage;
    [SerializeField] public float Speed;
    [SerializeField] private LayerMask _targetLayer;
    public Action<Vector3> OnHandleHit;
    
   // private BulletVisual _visual;
    //private BulletSpawner _spawner;

    
    public void Initialize(int damage,float  speed, Vector2 direction, TeamType team)
    {
        Damage = damage;
        Speed = speed;
        Direction = direction;
        Team = team;

        SetupLayer(team);
        SetupVisual(team);
    }

    private void SetupLayer(TeamType team)
    {
        gameObject.layer = team switch
        {
            TeamType.None => LayerMask.NameToLayer("Default"),
            TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
            TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetupVisual(TeamType team)
    {
        // Визуальная настройка делегируется отдельному компоненту
        var visual = GetComponent<BulletVisual>();
        visual?.SetTeamColor(team);
    }
    public class BulletMover : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _bulletTransform;

        private void Awake()
        {
            _bullet = GetComponent<Bullet>();
            _bulletTransform = transform;
            this.enabled = true;
            if (_bullet == null)
                Debug.LogError("Bullet component missing on " + gameObject.name);
        }
        private void Update()
        {
            Move(Time.deltaTime * 200);
            Debug.Log("Move delt");
        }

        public void Move(float deltaTime)
        {
            Vector3 moveStep = _bullet.Direction * _bullet.Speed * deltaTime;
            Debug.Log($"Direction{_bullet.Direction},_bullet.Speed{_bullet.Speed},deltaTime{deltaTime}");
            this.transform.position += moveStep;
            _bullet.transform.rotation = Quaternion.LookRotation(_bullet.Direction, Vector3.forward);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsValidTarget(other))
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(Damage, Team);
            HandleHit();
        }
    }

    private bool IsValidTarget(Collider2D other)
    {
        return ((1 << other.gameObject.layer) & _targetLayer) != 0;
    }

    private void HandleHit()
    {
        OnHandleHit?.Invoke(this.transform.position);
      //  _visual?.PlayExplosionVFX(transform.position);
        
    }
    public void SetOrientation(Bullet bullet, Vector2 position, Vector2 direction)
    {
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
    }
}
