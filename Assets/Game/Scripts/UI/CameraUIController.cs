using UnityEngine;
using Modules.Utils;

public class CameraUIController : MonoBehaviour
{
    [SerializeField] private CameraShaker _cameraShaker;

    [SerializeField] private HealthComponent _healthComponent;

    private void OnEnable()
    {
        _healthComponent.OnHealthChanged += this.OnHealthComponentChanged;
    }

    private void OnDisable()
    {
        _healthComponent.OnHealthChanged -= this.OnHealthComponentChanged;
    }

    private void OnHealthComponentChanged(int health)
    {
        _cameraShaker.Shake();
    }
}