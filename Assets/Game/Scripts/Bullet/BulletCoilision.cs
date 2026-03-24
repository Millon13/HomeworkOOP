using log4net.Util;
using UnityEngine;

public class BulletCoilision:MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    private Bullet _bullet;
    private BulletVisual _visual;
    private BulletSpawner _spawner;

    private void Awake()
    {
        _bullet = GetComponent<Bullet>();
        _visual = GetComponent<BulletVisual>();
       // _spawner = FindObjectOfType<BulletSpawner>(); // или внедрить через конструктор
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsValidTarget(other))
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_bullet.Damage, _bullet.Team);
            HandleHit();
        }
    }

    private bool IsValidTarget(Collider2D other)
    {
        return ((1 << other.gameObject.layer) & _targetLayers) != 0;
    }

    private void HandleHit()
    {
        _visual?.PlayExplosionVFX( transform.position);
        _spawner?.ReturnBullet(_bullet);
    }
  
}
