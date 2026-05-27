using Game;
using System;
using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour
{
    public event Action<int> OnHealthChanged;


    [Header("Health")] public bool isAlive;

    public int currentHealth;

    [SerializeField] protected ShipViewConfig _viewConfig;
    public event Action OnDead;

    public void TakeDamage(int damage)
    {
        if (!isAlive)
            return;


        if (damage <= 0)
            return;

        currentHealth -= damage;

        OnHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            isAlive = false;
            OnDead?.Invoke();
        }
    }
}