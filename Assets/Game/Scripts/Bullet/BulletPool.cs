using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletPool:MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _container;

    private readonly Stack<Bullet> _pool = new();
    public void Awake()
    {
        for (var i = 0; i < 10; i++)
        {

            Bullet bullet = Instantiate(_bulletPrefab, _container);
            bullet.gameObject.SetActive(false);
            _pool.Push(bullet);
        }
    }
    public void PoolPush(Bullet bullet)
    {
        _pool.Push(bullet);
    }
    public void TryPop()
    {
        if (_pool.TryPop(out Bullet bullet))
            bullet.gameObject.SetActive(true);
        else
            bullet = Instantiate(_bulletPrefab, _container);
    }
    public void SetOrientation(Bullet bullet, Vector2 position, Vector2 direction)
    {
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
    }
   
}
