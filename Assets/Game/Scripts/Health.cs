using Game;
using System;
using UnityEngine;

public class Health:MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    // public event Action OnDead;
    [Header("Health")]
    public bool isAlive;
     public int currentHealth;
    [SerializeField] private Animations _animations;
    [SerializeField] protected ShipControllerViewConfig _viewConfig;
    public event Action OnDead;
    public Transform _viewTransform;

    public void DeadCheking()
    {
        if (currentHealth <= 0)
        {
            isAlive = false;

        }
    }
   /* public void NotifyAboutDead()
    {


        ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
        Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

        this.OnDead?.Invoke();
    }*/



}
