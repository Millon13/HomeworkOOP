using Game;
using Modules.Utils;
using System;
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
    [SerializeField] private BulletVisual _bulletVisual;
    private readonly List<Bullet> _bullets = new();
    
    private void FixedUpdate()
    {
      //  Spawn();
    }
    private void Awake()
    {
       /* Bullet bullet = null;
        Vector2 direction =bullet.Direction;
        TeamType team = TeamType.None;
        bullet.Initialize(_bulletConfig, direction, team);*/
    }


    public void Spawn(BulletConfig _bulletConfig,Bullet _bullet, TeamType _team)
    {
        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
            
            Bullet bullet = _bullets[i];
           
            bullet.Initialize(_bulletConfig, _bullet.Direction, _team);

            if (!_levelBounds.InBounds(bullet.transform.position))
            {
                _bullets.RemoveAt(i);


                _bulletPool.PoolPush(bullet);
            }
            Vector2 direction = bullet.Direction;
            bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
         
            
            
        }
    }
  

    public void AddBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
    }
    public void ReturnBullet(Bullet bullet)
    {
        _bullets.Remove(bullet);
    }
}
