using Game;
using System;
using UnityEngine;

public class Health:MonoBehaviour
{
    public event Action<int> OnHealthChanged;
   // public event Action OnDead;
    [Header("Health")]
     public int currentHealth;
    [SerializeField] private Animations _animations;
    [SerializeField] protected ShipControllerViewConfig _viewConfig;
    
    
   
    
}
