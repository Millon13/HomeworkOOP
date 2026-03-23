using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerBulletInstantiator : MonoBehaviour
    {
        [SerializeField]
        private Fire _playerFire;

        [SerializeField]
        private BulletFire _playerBulletFire;
        [SerializeField] Bullet bullet;
        [SerializeField] BulletConfig _bulletConfig;
        [SerializeField] TeamType _teamType;
        Vector3 direction => bullet.Direction;

        //[SerializeField]
        //private BulletSpawner _bulletSpawner;
        private void OnEnable()
        {
            _playerFire.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            _playerFire.OnFire -= this.OnFire;
        }

        private void OnFire(BulletSpawner _bulletSpawner)
        {
            _bulletSpawner.Spawn(_bulletConfig, bullet, _teamType,direction
            
            );
            Debug.Log("OnFire In BulletInstantiator");
        }
    }
}