using Codice.CM.Common;
using Game;
using Modules.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletSpawner:MonoBehaviour
{
    [Header("Spawn")]
    private float _minSpawnCooldown = 2;
    private float _maxSpawnCooldown = 3;
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float _spawnTime;
    [SerializeField] private TransformBounds _levelBounds;
    [SerializeField] private BulletPool _bulletPool;

    private readonly List<Bullet> _bullets = new();
 
    [Header("Pool")]
    [SerializeField]
    private Enemy _prefab;

    [SerializeField]
    private Transform _container;

    private readonly Queue<Enemy> _pool = new();


  

    [Header("Points")]
    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private Transform[] _attackPositions;

    private int _spawnIndex;
    private int _attackIndex;

  
    private void Awake()
    {
        _spawnPositions.Shuffle();
        _attackPositions.Shuffle();

    }


    public void Spawn(Vector2 position,Vector2 direction,int damage, float speed)
    {
        //_bulletPool.TryPop();
        //_bullet.Initialize(damage, speed, direction,type);
        // _bulletPool.SetOrientation(_bullet, position, direction);
        Bullet bullet = _bulletPool.TryPop(position, direction, damage, speed);
       
       
    }
    public Bullet Spawn(Vector2 position, Vector2 direction, int damage, float speed, TeamType team)
    {
        Bullet bullet = _bulletPool.GetBullet();
        bullet.Initialize(damage, speed, direction, team, position);
        bullet.gameObject.SetActive(true);
        _bulletPool.AddActiveBullets(bullet);
        return bullet;
    }
    public void AddBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
    }
    public void ReturnBullet(Bullet bullet)
    {
        _bullets.Remove(bullet);
    }
    private Vector3 NextSpawnPosition()
    {
        if (_spawnIndex >= _spawnPositions.Length)
        {
            _spawnPositions.Shuffle();
            _spawnIndex = 0;
        }

        return _spawnPositions[_spawnIndex++].position;
    }

    private Vector3 NextDestination()
    {
        if (_attackIndex >= _attackPositions.Length)
        {
            _attackPositions.Shuffle();
            _attackIndex = 0;
        }

        return _attackPositions[_attackIndex++].position;
    }
    private IEnumerator DespawnInNextFrame(Enemy enemy)
    {
        yield return null;
        enemy.gameObject.SetActive(false);
        _pool.Enqueue(enemy);
    }
}
