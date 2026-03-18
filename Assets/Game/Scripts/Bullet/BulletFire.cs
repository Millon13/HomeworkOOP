using Game;
using System;
using UnityEngine;
public class BulletFire:Audio//сделать пул пуль
{

    [Header("Combat")]
   
    public float bulletSpeed;
    public int bulletDamage;
    public float _fireTime;
    public ShipControllerSO config;
    [SerializeField] PlayerShip playerShip;
    [SerializeField] Enemy enemy;

    [SerializeField]
    private ParticleSystem _fireVFX;

 
    public void Fire()//он же должен стрелять
    {
        float time = Time.time;
        
        if (time - _fireTime < config.FireCooldown )
            return;

        if (_fireSFX)
            _audioSource.PlayOneShot(_fireSFX);

        if (_fireVFX)
            _fireVFX.Play();

        playerShip.Fire(playerShip);
        _fireTime = time;
    }
}



