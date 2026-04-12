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
   
    private BulletConfig config;
    [Header("Combat")]
    Vector3 direction => bullet.Direction;
    Vector2 position => bullet.transform.position;
    int damage;// => bullet.Damage;
    float speed; //=> bullet.Speed;

   
    [SerializeField] float _fireCooldownDuration;

    public float _fireCooldown;

    public Transform _firePoint;


    [SerializeField] ShipController shipController;


    // [SerializeField]
    // private ParticleSystem _fireVFX; сделать через подписку на стрельбу
    private void Awake()
    {
        _fireCooldown = Time.time - _fireCooldownDuration;
    }


    public void FireTo(Vector2 spawnPosition, Vector2 direction)//он же должен стрелять
    {
        if (bullet != null)
        {

            _bulletSpawner.Spawn(position, direction, damage, speed);
            if (_bulletSpawner != null && _firePoint != null)
            {

                //DoFire();

                float time = Time.time;
                if (time - _fireCooldown < _fireCooldownDuration)
                    return;
                _bulletSpawner.Spawn(
                    spawnPosition,
                    direction,
                    damage,
                    speed
                );

                _fireCooldown = time;
                //  bullet.Move(time);








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

