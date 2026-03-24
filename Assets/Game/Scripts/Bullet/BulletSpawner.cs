using Game;
using Modules.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner:MonoBehaviour
{
    [Header("Spawn")]
    private float _minSpawnCooldown = 2;
    private float _maxSpawnCooldown = 3;
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float _spawnTime;
    [SerializeField] private TransformBounds _levelBounds;
    [SerializeField] private BulletPool _bulletPool;
   
    [SerializeField] private BulletVisual _bulletVisual;
    private readonly List<Bullet> _bullets = new();
    [SerializeField]private  BulletMover _bulletMover;
    [Header("Pool")]
    [SerializeField]
    private Enemy _prefab;

    [SerializeField]
    private Transform _container;

    private readonly Queue<Enemy> _pool = new();


    [Header("Target")]
    [SerializeField]
    private PlayerShip _player;

    [Header("Points")]
    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private Transform[] _attackPositions;

    private int _spawnIndex;
    private int _attackIndex;

    [Header("Bullets")]
    
    [SerializeField] private BulletMover bulletMover;

    private void FixedUpdate()
    {
        //  Spawn();


        for (int i = _bullets.Count - 1; i >= 0; i--)
        {

            Bullet bullet = _bullets[i];
           
            if (!_levelBounds.InBounds(bullet.transform.position))
            {
                _bullets.RemoveAt(i);
                bullet.gameObject.SetActive(false);
                

                


            }
            ;
           // bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);



        }
    }
  
    private void Awake()
    {
        _spawnPositions.Shuffle();
        _attackPositions.Shuffle();

    }


    public void Spawn( Bullet _bullet,int damage,float speed, Vector2 position, Vector2 direction, TeamType type)
    {
        _bulletPool.TryPop();
        _bullet.Initialize(damage, speed, direction,type);
        _bulletPool.SetOrientation(_bullet, position, direction);
        
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
