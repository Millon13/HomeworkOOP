using Modules.Utils;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager:BulletPool
{
    [SerializeField] private TransformBounds _levelBounds;

    private readonly List<Bullet> _activeBullets = new();


    public void FixedUpdate()
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet= _activeBullets[i];

            if (bullet == null || !bullet.gameObject.activeSelf)
            {
                _activeBullets.RemoveAt(i);
                continue;
            }

            if (_levelBounds != null && !_levelBounds.InBounds(bullet.transform.position))
            {

                ReturnToPool(bullet);

            }
        }
    }
}
