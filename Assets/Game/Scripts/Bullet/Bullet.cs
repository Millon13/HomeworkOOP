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
    public Action<Vector3> OnHit;
    public Action<Bullet> OnReturnToPool;


    [SerializeField] private Transform _bulletTransform;
   [SerializeField] private BulletVisual _visual;
    private BulletPool _pool;
    private bool hitDetected;
    
    public void Initialize(int damage,float  speed, Vector2 direction, TeamType team,Vector2 position)
    {
        Damage = damage;
        Speed = speed;
        Direction = direction;
        Team = team;
        transform.position = position;
        SetupLayer(team);
        SetupVisual(team);

        // Находим пул
        if (_pool == null)
            _pool = FindObjectOfType<BulletPool>();
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


    private void Awake()
    {
        this.GetComponent<Bullet>();
        _bulletTransform = transform;
        this.enabled = true;
        if (this == null)
            Debug.LogError("Bullet component missing on " + gameObject.name);
    }
    private void Update()
    {
        
        Move(Time.deltaTime);
        Debug.Log("Move delt");
    }

    public void Move(float deltaTime)
    {

        Vector3 moveStep = /*(Vector3)Direction*/new Vector3(Direction.x, Direction.y, 0) * Speed * deltaTime;
        Debug.Log($"Direction{Direction},_bullet.Speed{Speed},deltaTime{deltaTime}");
        transform.position += moveStep;
        //transform.rotation = Quaternion.LookRotation(Direction, Vector3.forward);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool valid = IsValidTarget(other);
        Debug.Log($"validLayer{valid}");
        if (!IsValidTarget(other))
            return;
       
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(Damage, Team);
            hitDetected = true;
            
           
        }
        if (other.TryGetComponent(out Health health))
        {
            
            health.TakeDamage(Damage);
            hitDetected = true;
        }
        if (hitDetected)
        {
            HandleHit();
            OnReturnToPool.Invoke(this);
        }
        
    }
   

    private void OnDisable()
    {
        // Очищаем ссылки при деактивации
        _pool = null;
    }
    private bool IsValidTarget(Collider2D other)
    {
        return ((1 << other.gameObject.layer) & _targetLayer) != 0;
    }

    private void HandleHit()
    {
       OnHit?.Invoke(this.transform.position);
      _visual?.PlayExplosionVFX(transform.position);
      
        
    }
    public void SetOrientation(Bullet bullet, Vector2 position, Vector2 direction)
    {
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
    }
}
