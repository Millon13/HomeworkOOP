using Game;
using Modules.UI;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Modules.Utils;
using Random = UnityEngine.Random;
using System.Collections;
using System;
using Codice.Client.Common.GameUI;

public class EnemySpawner: MonoBehaviour, IEnemyDespawner
{
    [Header("Spawn")]
  
    [SerializeField] private Cooldown cooldown;

    [SerializeField] CooldownConfig cooldownConfig;

    private int  _destroyedEnemies;

    public Action<int> OnAddScore;

    [Header("Pool")]
    [SerializeField]
    private Enemy _prefab;

    [SerializeField] private ShipController _enemy;

    private readonly Queue<Enemy> _pool = new();


    [SerializeField]
    private Transform _container;
    [SerializeField] private SpawnPosition position;

    private void Update()
    {
        Spawner();
    }
    private void Spawner()
    {
        
       
        if (cooldown != null && cooldown.IsSpawnReady())
        {
            if (_pool.TryDequeue(out Enemy enemy))
                enemy.gameObject.SetActive(true);
            else
                enemy = Instantiate(_prefab, _container);


            enemy.transform.position = position.NextSpawnPosition();

            enemy.destination = position.NextDestination();

            enemy.SetDespawner(enemy._despawner);


            cooldown.ResetSpawnCooldown();
        }
    }
  

    public void Despawn(Enemy enemy)
    {
       
        _destroyedEnemies++;

        OnAddScore.Invoke(_destroyedEnemies);

        StartCoroutine(DespawnInNextFrame(enemy));
    }

    private IEnumerator DespawnInNextFrame(Enemy enemy)
    {
        yield return null;

        enemy.gameObject.SetActive(false);

        _pool.Enqueue(enemy);
    }
    

}
