using UnityEngine;
using System;
using Game;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction;

    private TeamType _team;

    private BulletConfig _config;

    public Action<Vector3> OnHit;

    public Action<Bullet> OnReturnToPool;

    private bool hitDetected;

    public void Initialize(BulletConfig config, Vector2 position, Vector2 direction, TeamType team)
    {
        _config = config;
        hitDetected = false;
        _team = team;
        _direction = direction;
        transform.position = position;
        this.enabled = true;
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
        if (other.TryGetComponent(out HealthComponent health))
        {
            health.TakeDamage(_config.Damage);
            hitDetected = true;
        }

        if (hitDetected)
        {
            HandleHit();
        }
    }


    private void HandleHit()
    {
        OnHit?.Invoke(this.transform.position);
        OnReturnToPool?.Invoke(this);
    }

    public void SetOrientation(Bullet bullet, Vector2 position, Vector2 direction)
    {
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            direction = direction.normalized;
            SetOrientation(this, this.transform.position, direction);
        }
    }
}