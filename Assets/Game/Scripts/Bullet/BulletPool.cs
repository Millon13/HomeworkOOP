using Game;
using Modules.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletPool:MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private TransformBounds _levelBounds;
    [SerializeField]private int poolsize;
    private int damage;
    private float speed;
    private Vector2 direction;
    private Vector2 position;
    private TeamType type;
    private readonly Stack<Bullet> _pool = new();
    private readonly List<Bullet> _activeBullets = new();
    public void Awake()
    {
        //var config = GetComponent<BulletViewConfig>();
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
    public void ReturnToPool(Bullet bullet)
    {
        if (bullet == null) return;

       // bullet.OnHit -= HandleBulletHit;
        bullet.gameObject.SetActive(false);
        _pool.Push(bullet);
        _activeBullets.Remove(bullet);
    }
    public void PoolPush(Bullet bullet)
    {
        _pool.Push(bullet);
    }
    public Bullet TryPop(Vector2 position,Vector2 direction,int damage,float speed)
    {
        if (_pool.TryPop(out Bullet bullet))
            bullet.gameObject.SetActive(true);
        else
            bullet = Instantiate(_bulletPrefab, _container);
        bullet.Initialize(damage, speed, direction, type);
        return bullet;
    }
    public void FixedUpdate()
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = _activeBullets[i];

            if (bullet == null || !bullet.gameObject.activeSelf)
            {
                _activeBullets.RemoveAt(i);
                continue;
            }

            if (_levelBounds != null && !_levelBounds.InBounds(bullet.transform.position))
            {
                // ¬озвращаем пулю в пул при выходе за границы
                this.ReturnToPool(bullet);
                _activeBullets.RemoveAt(i);
            }
        }
    }

}
