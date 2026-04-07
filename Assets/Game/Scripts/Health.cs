using Game;
using System;
using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    // public event Action OnDead;
    [Header("Health")]
    public bool isAlive;
    public int currentHealth;

    [SerializeField] protected ShipControllerViewConfig _viewConfig;
    public event Action OnDead;
    public Transform _viewTransform;
    private IDamageHandler _damageHandler;

    public void SetDamageHandler(IDamageHandler handler) => _damageHandler = handler;
    public interface IDamageHandler
    {
        int Handle(int damage);
    }
    public void DeadCheking()
    {
        if (currentHealth <= 0)
        {
            isAlive = false;
            Destroy(gameObject);

        }
    }
    public void TakeDamage(int damage)
    {
        if (!isAlive)
            return;

        if (_damageHandler != null)
            damage = _damageHandler.Handle(damage);

        if (damage <= 0)
            return;
       
        currentHealth -= damage;

        //OnHealthChanged?.Invoke(currentHealth);
        StartCoroutine(InvokeHealth(currentHealth));
        if (currentHealth <= 0)
        {
           
            isAlive = false;
            OnDead?.Invoke();
        }

            
   
                
    }
    private IEnumerator InvokeHealth(int health)
    {
        {
            yield return null; 
            try
            {
                if (this != null && gameObject != null)
                    OnHealthChanged?.Invoke(health);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error in OnHealthChanged event: {e.Message}");
            }
        }
    }
    
    /* public void NotifyAboutDead()
     {


         ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
         Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

         this.OnDead?.Invoke();
     }*/



}
