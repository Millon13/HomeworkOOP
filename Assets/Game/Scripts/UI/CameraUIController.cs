using UnityEngine;
using Game;
using Modules.UI;
using Modules.Utils;

public class CameraUIController : MonoBehaviour
{
    [SerializeField] private CameraShaker _cameraShaker;

    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.OnHealthChanged += this.OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= this.OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        _cameraShaker.Shake();
    }
}