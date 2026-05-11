using Codice.CM.Common;
using Game;
using Modules.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



[RequireComponent(typeof(SpawnPosition))]
public class BulletSpawner:MonoBehaviour
{
    [SerializeField] private SpawnPosition position;
 
    [SerializeField] private TransformBounds _levelBounds;

    [SerializeField] private BulletPool _bulletPool;

    [SerializeField] private BulletConfig config;

    [SerializeField] private BulletViewConfig viewConfig;

    private readonly List<Bullet> _bullets = new();
 
    [Header("Pool")]
    [SerializeField]
    private Enemy _prefab;

    [SerializeField]
    private Transform _container;

    private readonly Queue<Enemy> _pool = new();

    public Bullet Spawn(Vector2 spawnPosition,Vector2 direction)
    {
        Bullet bullet = _bulletPool.GetBullet(config, viewConfig,direction);
        bullet.transform.position = spawnPosition;
        SetBulletLayer(bullet);
        bullet.SetDirection(direction);

        return bullet;
     
    }
    private void SetBulletLayer(Bullet bullet)
    {
        
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bullet.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
           
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            bullet.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
           
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

    private IEnumerator DespawnInNextFrame(Enemy enemy)
    {
        yield return null;
        enemy.gameObject.SetActive(false);
        _pool.Enqueue(enemy);
    }
}
