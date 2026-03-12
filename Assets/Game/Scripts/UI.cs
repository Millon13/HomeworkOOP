using Modules.UI;
using UnityEngine;

public class UI
{
    [Header("UI")]
    [SerializeField]
    private ScoreView _scoreView;

    private int _destroyedEnemies;
    private void Awake()
    {
        _scoreView.SetValue(_destroyedEnemies);
    }

}
