using UnityEngine;
using Game;
using Modules.UI;
using Modules.Utils;

public class GameOverUIController:MonoBehaviour
{

    [SerializeField]
    private GameOverView _gameOverView;
    [SerializeField]private Health _health;

    private void OnEnable()
    {
       

        if (_gameOverView != null)
            _health.OnDead += _gameOverView.Show;

       

    } private void OnDisable()
    {


        if (_gameOverView != null)
            _health.OnDead -= _gameOverView.Show;
    }
}
