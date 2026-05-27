using Modules.Utils;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private TransformBounds _levelBounds;

    private readonly List<Bullet> _activeBullets = new();

    [SerializeField] private Pool _pool;

    [SerializeField] private TeamType _teamType;

    private readonly List<Bullet> _bullets = new();

    [SerializeField] private Transform _container;


    public void FixedUpdate()
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = _activeBullets[i];

            if (bullet == null || !bullet.gameObject.activeSelf)
            {
                _activeBullets.RemoveAt(i);
                continue;
            }

            if (_levelBounds != null && !_levelBounds.InBounds(bullet.transform.position))
            {
                _pool.ReturnToPool(bullet.gameObject);
            }
        }
    }


    public Bullet Spawn(BulletConfig config, Vector2 spawnPosition, Vector2 direction)
    {
        Bullet bullet = _pool.Get<Bullet>();
        bullet.Initialize(config, spawnPosition, direction, _teamType);
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
}