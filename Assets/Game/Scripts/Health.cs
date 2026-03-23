using Game;
using System;
using UnityEngine;

public class Health:MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    [Header("Health")]
     public int currentHealth;
    [SerializeField] private Animations _animations;
    [SerializeField] protected ShipControllerViewConfig _viewConfig;
    public void NotifyAboutHealthChanged(int health)
    {
        if (health > 0)
            _animations.AnimateDamage(_viewConfig);

        this.OnHealthChanged?.Invoke(health);
    }
    
}
