using UnityEngine;
using Modules.UI;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private EnemySpawner _spawner;


    private void OnEnable()
    {
        _spawner.OnAddScore += OnScoreChanged;
    }

    private void OnDisable()
    {
        _spawner.OnAddScore -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _scoreView.SetValue(score);
    }
}