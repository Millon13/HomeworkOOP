using Modules.Utils;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class BulletManager : MonoBehaviour //сделать сиглтоном
{
    [SerializeField] private TransformBounds _levelBounds;

    private readonly List<Bullet> _activeBullets = new();

    [SerializeField] private Pool _pool;

    public static BulletManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

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


    public void Spawn(BulletConfig config, Vector2 spawnPosition, Vector2 direction, TeamType team)
    {
        Bullet bullet = _pool.Get<Bullet>();
        _activeBullets.Add(bullet);
        bullet.Initialize(config, spawnPosition, direction, team);
    }
}