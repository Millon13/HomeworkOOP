using Game;
using System;
using UnityEngine;


public class FireComponent : MonoBehaviour
{
    public event Action OnFire;

    public bool CanFire { get; set; }

    private BulletManager _bulletManager;

    [SerializeField] private BulletConfig config;

    [SerializeField] float _fireCooldownDuration;

    public float _fireCooldown;

    public Transform _firePoint;

    [SerializeField] Ship _ship;
    [SerializeField] private TeamType _type;

    private void Awake()
    {
        _fireCooldown = Time.time - _fireCooldownDuration;
        _bulletManager = BulletManager.Instance;
    }

    public void FireUp()
    {
        this.FireTo((Vector2.up));
    }


    public void FireTo(Vector2 direction)
    {
        if (_bulletManager != null && _firePoint != null && CanFire)
        {
            float time = Time.time;
            if (time - _fireCooldown < _fireCooldownDuration)
                return;
            _bulletManager.Spawn(config, _firePoint.position, direction, _type
            );
            _fireCooldown = time;
            this.OnFire?.Invoke();
        }
    }
}