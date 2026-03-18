using Game;
using System;
using UnityEngine;

public class Fire:MonoBehaviour
{
    public event Action<BulletSpawner> OnFire;
    [SerializeField] BulletSpawner _bulletSpawner;
    public void DoFire()
    {
        this.OnFire?.Invoke(_bulletSpawner);
    }
}
