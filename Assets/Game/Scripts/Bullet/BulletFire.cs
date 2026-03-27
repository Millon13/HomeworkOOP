using Game;
using System;
using UnityEngine;
public class BulletFire:MonoBehaviour//сделать пул пуль
{

    [Header("Combat")]
   
    public float bulletSpeed;
    public int bulletDamage;
    public float _fireTime;
    public ShipControllerSO config;
    [SerializeField] TeamType type;
    [SerializeField] PlayerShip playerShip;
    [SerializeField] Enemy enemy;
    [SerializeField] Fire fire;
    
    [SerializeField]
    private ParticleSystem _fireVFX;

    private void Update()
    {
        if (type == TeamType.Enemy)
            Fire();
        else
            return;
    }


    public void Fire()//он же должен стрелять
    {
        float time = Time.time;
        
        if (time - _fireTime < config.FireCooldown )
            return;

       // if (_fireSFX)
          //  _audioSource.PlayOneShot(_fireSFX);

       // if (_fireVFX)
       //     _fireVFX.Play();
        fire.DoFire();
       
        _fireTime = time;
    }
   /* public void TimeFire(float time)
    {
        if (!isNotReached)
        {
            time = Time.time;
            if (time - _fireTime >= _fireCooldown)
            {
                fire.DoFire();
                _fireTime = time;
            }

        }}
   */
    
}



