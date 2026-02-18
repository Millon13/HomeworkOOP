using Game;
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

       shipController.FireAction();
        _fireTime = time;
    }
}

public  interface IFire
{
    void Fire();
}
