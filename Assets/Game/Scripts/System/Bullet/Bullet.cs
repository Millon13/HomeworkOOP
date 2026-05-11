using UnityEngine;
using Modules.Utils;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Game;
using PlasticGui;

public class Bullet:MonoBehaviour
{
    private Vector2 _direction;

    private BulletConfig _config;

    private BulletViewConfig _viewConfig;

    public Action<Vector3> OnHit;

    public Action<Bullet> OnReturnToPool;

    [SerializeField] private Transform _bulletTransform;

    private bool hitDetected;
    
    public void Initialize(BulletConfig config,BulletViewConfig viewConfig, Vector2 direction)
    {
        BulletVisual _visual = GetComponent<BulletVisual>();
        _config = config;
        _viewConfig = viewConfig;
        hitDetected = false;
        _direction = direction;

        if (_visual != null)
        {
            _visual.Initialize(config, viewConfig);
        }

        transform.position = new Vector3(_config.Position.x, _config.Position.y, 0);
        this.enabled = true;

    }

    private void Awake()
    {
        _bulletTransform = transform;
        BulletVisual _visual = GetComponent<BulletVisual>();
        
    }
    private void Update()
    {
        Move(Time.deltaTime);
        Debug.Log("Move delt");
    }

    public void Move(float deltaTime)
    {

        Vector3 moveStep = new Vector3(_direction.x, _direction.y, 0) * _config.Speed * deltaTime;
        Debug.Log($"Direction{_direction.x},_bullet.Speed{_direction.y},deltaTime{deltaTime}");
        transform.position += moveStep;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            
            health.TakeDamage(_config.Damage);
            hitDetected = true;
        }
        if (hitDetected)
        {
            HandleHit();
            
        }   
    }

    private bool IsValidTarget(Collider2D other)
    {
        return ((1 << other.gameObject.layer) & _config.TargetLayer) != 0;
    }

    private void HandleHit()
    {
       OnHit?.Invoke(this.transform.position); 
    }
    public void SetOrientation(Bullet bullet, Vector2 position, Vector2 direction)
    {

        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
    }
    public void SetDirection( Vector2 direction)
    {

        if (direction != Vector2.zero)
        {
            direction = direction.normalized;

            SetOrientation(this, this.transform.position, direction);
        }
    }

}
