using Game;
using Modules.UI;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Modules.Utils;
using Random = UnityEngine.Random;
using System.Collections;
using Codice.Client.Common.GameUI;

public class EnemySpawner: MonoBehaviour, IEnemyDespawner
{ [Header("Spawn")]
        [SerializeField]
        private float _minSpawnCooldown = 2;

        [SerializeField]
        private float _maxSpawnCooldown = 3;
        
        private float _spawnCooldown;
        private float _spawnTime;
    private int _destroyedEnemies;
    [Header("Pool")]
    [SerializeField]
    private Enemy _prefab;
    [SerializeField] private ShipController _enemy;

    private readonly Queue<Enemy> _pool = new();


    [SerializeField]
    private Transform _container;
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

    private void Start()
    {
        this.ResetSpawnCooldown();
        
    }
    private void Update()
    {
        Spawner();
    }
    private void Spawner()
    {
        float time = Time.fixedTime;
        if (time - _spawnTime < _spawnCooldown )
            return;

        if (_pool.TryDequeue(out Enemy enemy))
            enemy.gameObject.SetActive(true);
        else
            enemy = Instantiate(_prefab, _container);

        enemy.transform.position = this.NextSpawnPosition();
        enemy.destination = this.NextDestination();
        

        //enemy._target = _player;
        enemy.SetDespawner(enemy._despawner);
     
        this.ResetSpawnCooldown();
    }
    private void ResetSpawnCooldown()
    {
        _spawnCooldown = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
        _spawnTime = Time.fixedTime;
    }

    public void Despawn(Enemy enemy)
    {
       
        _destroyedEnemies++;
       
        StartCoroutine(DespawnInNextFrame(enemy));
    }

    private IEnumerator DespawnInNextFrame(Enemy enemy)
    {
        yield return null;
        enemy.gameObject.SetActive(false);
        _pool.Enqueue(enemy);
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

}
