using Game;
using Modules.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletPool:MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private Transform _container;
   
    [SerializeField]private int poolsize;

    private readonly Stack<Bullet> _pool = new();

    private readonly List<Bullet> _activeBullets = new List<Bullet>();


    public void Awake()
    {
        for (var i = 0; i <poolsize ; i++)
        {

            Bullet bullet = Instantiate(_bulletPrefab);

            if (_container != null && _container.gameObject.scene.isLoaded)
            {
                bullet.transform.SetParent(_container);
            }

            bullet.gameObject.SetActive(false);

            _pool.Push(bullet);
        }
    }
    public void OnEnable()
    {
        _bulletPrefab.OnReturnToPool += ReturnToPool;
    }
    public void ReturnToPool(Bullet bullet)
    {
        if (bullet == null) return;

        bullet.gameObject.SetActive(false);

         _activeBullets.Remove(bullet);
         _pool.Push(bullet);

      
    }
    public void PoolPush(Bullet bullet)
    {
        _pool.Push(bullet);
    }
    public Bullet TryPop(BulletConfig config, BulletViewConfig viewConfig,Vector2 direction)
    {
        if (_pool.TryPop(out Bullet bullet))
            bullet.gameObject.SetActive(true);
        else
            bullet = Instantiate(_bulletPrefab, _container);
        bullet.Initialize(config, viewConfig,direction);

        AddActiveBullets(bullet);

        return bullet;
    }

    public void AddActiveBullets( Bullet bullet)
    {
        _activeBullets.Add(bullet);
    }
    public Bullet GetBullet(BulletConfig config, BulletViewConfig viewConfig, Vector2 direction)
    {

        Bullet bullet = _pool.Count > 0 ? _pool.Pop() : CreateNewBullet();
        bullet.Initialize(config, viewConfig,direction);
        bullet.gameObject.SetActive(true);
        _activeBullets.Add(bullet);
        return bullet;
        
    }

    private Bullet CreateNewBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _container);
        bullet.OnReturnToPool += ReturnToPool;
        return bullet;
    }

}
