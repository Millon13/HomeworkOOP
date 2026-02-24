using Game;
using System;
using UnityEngine;
public class BulletFire:Audio, IShipFire
{

    [Header("Combat")]
   
    public float bulletSpeed;
    public int bulletDamage;
    public float _fireTime;
    public ShipControllerSO config;
    [SerializeField] ShipController shipController;

    [SerializeField]
    private ParticleSystem _fireVFX;

 
    public void Fire(ShipController shipController)
    {
        float time = Time.time;
        if (time - _fireTime < config.FireCooldown || shipController.currentHealth <= 0)
            return;

        if (_fireSFX)
            _audioSource.PlayOneShot(_fireSFX);

        if (_fireVFX)
            _fireVFX.Play();

        shipController.Fire(shipController);
        _fireTime = time;
    }
}



