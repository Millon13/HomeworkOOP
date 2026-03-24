using Game;
using System;
using UnityEngine;

public class Dead:MonoBehaviour
{
    public event Action OnDead;
    [SerializeField] protected ShipControllerViewConfig _viewConfig;
    public Transform _viewTransform;
   
    public void NotifyAboutDead()
    {


        ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
        Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

        this.OnDead?.Invoke();
    }
}
