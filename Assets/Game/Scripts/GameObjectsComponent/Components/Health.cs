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
