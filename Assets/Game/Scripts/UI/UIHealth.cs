using Modules.UI;
using UnityEngine;

public class UIHealth
{
    [SerializeField] private HealthComponentView _healthComponentView;
    [SerializeField] private HealthView _healthView;

    public void HealthUI(int health)
    {
            if (_healthView != null)
            _healthView.SetHealth(health, 10);
    }
}
