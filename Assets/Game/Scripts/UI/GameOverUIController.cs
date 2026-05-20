using UnityEngine;
using Game;
using Modules.UI;
using Modules.Utils;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _player;
    [SerializeField] private Image _endImage;

    private void OnEnable()
    {
        if (_gameOverView != null)
            _health.OnDead += _gameOverView.Show;
        _health.OnDead += PlayerVanished;
        _health.OnDead += EndImage;
    }

    private void OnDisable()
    {
        if (_gameOverView != null)
            _health.OnDead -= _gameOverView.Show;
        _health.OnDead -= PlayerVanished;
        _health.OnDead -= EndImage;
    }

    private void PlayerVanished()
    {
        Destroy(_player.gameObject);
    }

    private void EndImage()
    {
        _endImage.enabled = true;
    }
}