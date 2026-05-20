using UnityEngine;
using Game;
using Modules.UI;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;
using Modules.Utils;
using Random = UnityEngine.Random;
using System.Collections;
using Codice.Client.Common.GameUI;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPositions;

    [SerializeField] private Transform[] _attackPositions;

    private int _spawnIndex;
    private int _attackIndex;

    private void Awake()
    {
        _spawnPositions.Shuffle();
        _attackPositions.Shuffle();
    }

    public Vector3 NextSpawnPosition()
    {
        if (_spawnIndex >= _spawnPositions.Length)
        {
            _spawnPositions.Shuffle();
            _spawnIndex = 0;
        }

        return _spawnPositions[_spawnIndex++].position;
    }

    public Vector3 NextDestination()
    {
        if (_attackIndex >= _attackPositions.Length)
        {
            _attackPositions.Shuffle();
            _attackIndex = 0;
        }

        return _attackPositions[_attackIndex++].position;
    }
}