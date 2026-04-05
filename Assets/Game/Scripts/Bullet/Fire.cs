using Game;
using System;
using UnityEngine;


public class Fire : MonoBehaviour
{
    public event Action<BulletSpawner> OnFire;
    public event Action OnFireAnim;
    public event Action OnAnimDamage;
    public bool CanFire;
    [SerializeField] BulletSpawner _bulletSpawner;
    [SerializeField] private Bullet bullet;
    // [SerializeField] Bullet bullet;

    [Header("Combat")]
    Vector3 direction => bullet.Direction;
    Vector2 position => bullet.transform.position;
    int damage => bullet.Damage;
    float speed => bullet.Speed;

    //public float bulletSpeed;
    // public int bulletDamage;
    [SerializeField] float _fireCooldownDuration;
    public float _fireCooldown;
    public Transform _firePoint;
    public ShipControllerSO config;

    [SerializeField] ShipController shipController;


    // [SerializeField]
    // private ParticleSystem _fireVFX; сделать через подписку на стрельбу
    private void Awake()
    {
        _fireCooldown = Time.time - _fireCooldownDuration;
    }


    public void FireTo(Vector2 spawnPosition ,Vector2 direction)//он же должен стрелять
    {
       if(bullet!=null)
        {
           
            _bulletSpawner.Spawn(position, direction, damage, speed);
            if (_bulletSpawner != null && _firePoint != null)
            { 
                
                DoFire();
               
                

                _bulletSpawner.Spawn(
                    spawnPosition,
                    direction,
                    damage,
                    speed
                );

                float time = Time.time;
                bullet.Move(time);
                if (time - _fireCooldown < _fireCooldownDuration)
                    return;




               

                time = _fireCooldown;
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

