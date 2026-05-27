using UnityEngine;
using Modules.UI;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private Image _endImage;

    private void OnEnable()
    {
        if (_gameOverView != null)
            _healthComponent.OnDead += _gameOverView.Show;
        _healthComponent.OnDead += EndImage;
    }

    private void OnDisable()
    {
        if (_gameOverView != null)
            _healthComponent.OnDead -= _gameOverView.Show;
        _healthComponent.OnDead -= EndImage;
    }

    private void EndImage()
    {
        _endImage.enabled = true;
    }
}