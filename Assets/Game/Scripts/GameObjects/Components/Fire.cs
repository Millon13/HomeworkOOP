using Game;
using System;
using UnityEngine;


public class Fire : MonoBehaviour
{
    public event Action<BulletPoolManager> OnFire;
    [SerializeField] BulletPoolManager bulletManager;

    public bool CanFire;

    [SerializeField] BulletPoolManager _bulletManager;

    [SerializeField] private Bullet bullet;

    [SerializeField] private Transform target;

    private BulletConfig config;

    [SerializeField] float _fireCooldownDuration;

    public float _fireCooldown;

    public Transform _firePoint;

    [SerializeField] Ship _ship;

    private void Awake()
    {
        _fireCooldown = Time.time - _fireCooldownDuration;
    }


    public void FireTo(BulletConfig config, BulletViewConfig viewConfig, Vector2 direction)
    {
        if (bullet != null)
        {
            if (_bulletManager != null && _firePoint != null)
            {
                float time = Time.time;
                if (time - _fireCooldown < _fireCooldownDuration)
                    return;
                _bulletManager.Spawn(_firePoint.position, direction
                );

                _fireCooldown = time;
            }
        }
    }
}