using Game;
using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour, IEnemyDespawner
{
    [Header("Spawn")] [SerializeField] private SpawnCooldown cooldown;

    [SerializeField] CooldownConfig cooldownConfig;

    [SerializeField] private GameObject _target;

    private int _destroyedEnemies;
    public event Action<int> OnAddScore;

    [Header("Pool")] [SerializeField] private Enemy _prefab;

    [SerializeField] private Ship _enemy;

    [SerializeField] private Pool _pool;

    [SerializeField] private Transform _container;

    [SerializeField] private SpawnPosition position;

    private void Update()
    {
        Spawner();
    }

    private void Spawner()
    {
        if (cooldown != null && cooldown.IsSpawnReady())
        {
            Enemy enemy = _pool.Get<Enemy>();
            enemy.gameObject.SetActive(true);
            enemy.transform.position = position.NextSpawnPosition();
            enemy.destination = position.NextDestination();
            enemy.SetDespawner(this);
            enemy.SetTarget(_target);
            cooldown.ResetSpawnCooldown();
        }
    }


    public void Despawn(Enemy enemy)
    {
        _destroyedEnemies++;
        OnAddScore?.Invoke(_destroyedEnemies);
        StartCoroutine(DespawnInNextFrame(enemy));
    }

    private IEnumerator DespawnInNextFrame(Enemy enemy)
    {
        yield return null;
        if (enemy != null && enemy.gameObject != null)
        {
            Destroy(enemy.gameObject);
        }
    }
}