using UnityEngine;
using System;
using Game;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction;

    private TeamType _team;

    private BulletConfig _config;

    public event Action<Vector3> OnHit;

    public event Action<Bullet> OnDispose;

    private bool hitDetected;

    public void Initialize(BulletConfig config, Vector2 position, Vector2 direction, TeamType team)
    {
        _config = config;
        _team = team;
        hitDetected = false;
        _direction = direction;
        transform.position = position;
        this.enabled = true;

        if (_team == TeamType.Player)
        {
            this.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
        }
        else if (_team == TeamType.Enemy)
        {
            this.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
        }
    }


    private void Update()
    {
        Move(Time.deltaTime);
    }

    public void Move(float deltaTime)
    {
        Vector3 moveStep = new Vector3(_direction.x, _direction.y, 0) * _config.Speed * deltaTime;
        transform.position += moveStep;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health))
        {
            health.TakeDamage(_config.Damage);
            hitDetected = true;
        }

        if (hitDetected)
        {
            OnHit?.Invoke(this.transform.position);
            OnDispose?.Invoke(this);
        }
    }
}