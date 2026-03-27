using Game;
using System;
using UnityEngine;

public class Fire:MonoBehaviour
{
    public event Action<BulletSpawner> OnFire;
    public event Action OnFireAnim;
    public event Action OnAnimDamage;
    [SerializeField] BulletSpawner _bulletSpawner;
   // [SerializeField] Bullet bullet;
    
    /*[Header("Combat")]
    Vector3 direction => bullet.Direction;
    Vector2 position => bullet.transform.position;
    int damage => bullet.Damage;
    float speed => bullet.Speed;*/

    //public float bulletSpeed;
   // public int bulletDamage;
    public float _fireCooldown;
    public Transform firePoint;
    public ShipControllerSO config;
    
    [SerializeField] ShipController shipController;
    

   // [SerializeField]
   // private ParticleSystem _fireVFX; сделать через подписку на стрельбу

    private void Update()
    {
        /*if (shipController.teamType!=TeamType.None)
            FireTo();
        else
            return;*/
    }


    public void FireTo()//он же должен стрелять
    {
        if (shipController.teamType != TeamType.None)
        {
            _bulletSpawner.Spawn();
            float time = Time.time;

            if (time < _fireCooldown)
                return;




            DoFire();

            time = _fireCooldown;
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
