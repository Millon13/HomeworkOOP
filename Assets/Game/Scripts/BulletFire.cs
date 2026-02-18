using Game;
using System;
using UnityEngine;
public class BulletFire:Audio, IFire
{

    [Header("Combat")]
    public Transform firePoint;
    public float bulletSpeed;
    public int bulletDamage;
    public float _fireTime;
    public ShipControllerSO config;
    [SerializeField] ShipController shipController;
    public event Action<ShipController> OnFire;
    [SerializeField]
    private ParticleSystem _fireVFX;

    //[SerializeField] private Audio audio;
    public void Fire()
    {
        float time = Time.time;
        if (time - _fireTime < config.FireCooldown || shipController.currentHealth <= 0)
            return;

        if (_fireSFX)
            _audioSource.PlayOneShot(_fireSFX);

        if (_fireVFX)
            _fireVFX.Play();

        this.OnFire?.Invoke(shipController);
        _fireTime = time;
    }
}

public  interface IFire
{
    void Fire();
}
