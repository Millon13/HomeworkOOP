using UnityEngine;
using Game;
using Modules.UI;
using Modules.Utils;
using System;

public class ScoreController:MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private EnemySpawner _spawner;


    private void OnEnable()
    {
        _spawner.OnAddScore += OnScoreChanged;
    }
    private void OnScoreChanged(int score)
    {
        _scoreView.SetValue(score);
    }
}
