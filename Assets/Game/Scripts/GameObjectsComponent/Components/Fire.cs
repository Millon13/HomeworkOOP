using Game;
using System;
using UnityEngine;


public class Fire : MonoBehaviour
{
    public event Action<BulletSpawner> OnFire;
    [SerializeField] BulletPoolManager bulletManager;

    public event Action OnFireAnim;

    public event Action OnAnimDamage;

    public bool CanFire;

    [SerializeField] BulletSpawner _bulletSpawner;

    [SerializeField] private Bullet bullet;

    [SerializeField] private Transform target;

    private BulletConfig config;
   
    [SerializeField] float _fireCooldownDuration;

    public float _fireCooldown;

    public Transform _firePoint;

    [SerializeField] ShipController shipController;

    private void Awake()
    {
        _fireCooldown = Time.time - _fireCooldownDuration;
    }

  
    public void FireTo(BulletConfig config,BulletViewConfig viewConfig,Vector2 direction)
    {
        if (bullet != null)
        {
            
            if (_bulletSpawner != null && _firePoint != null)
            {

                
                float time = Time.time;
                if (time - _fireCooldown < _fireCooldownDuration)
                    return;
                _bulletSpawner.Spawn(_firePoint.position,direction

                );

                _fireCooldown = time;
               
            }
        }
    }



    public void DoFire()
    {
        this.OnFire?.Invoke(_bulletSpawner);
        this.OnFireAnim?.Invoke();

    }
    public void HandleHit()
    {
        this.OnAnimDamage?.Invoke();
        //_bulletSpawner?.ReturnBullet(bullet);
    }
}

