using Modules.Utils;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner:MonoBehaviour
{
    [Header("Spawn")]
    private float _minSpawnCooldown = 2;
    private float _maxSpawnCooldown = 3;
    private float _spawnCooldown;
    private float _spawnTime;
    [SerializeField] private TransformBounds _levelBounds;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private BulletConfig _bulletConfig;
    private readonly List<Bullet> _bullets = new();
    private void FixedUpdate()
    {
      //  Spawn();
    }
   
    public void Spawn()
    {
        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = _bullets[i];

            if (!_levelBounds.InBounds(bullet.transform.position))
            {
                _bullets.RemoveAt(i);


                _bulletPool.PoolPush(bullet);
            }
        }
    }
    public void ReturnBullet(Bullet bullet)
    {
        
    }
}
