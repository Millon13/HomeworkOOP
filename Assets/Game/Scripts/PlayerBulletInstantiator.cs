using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerBulletInstantiator : MonoBehaviour
    {
        //[SerializeField]
        //private BulletWorldGO _bulletWorld;
        [SerializeField]
        private BulletSpawner _bulletSpawner;

        [SerializeField]
        private PlayerShip _player;

        [SerializeField]
        private BulletFire _playerBulletFire;
        private void OnEnable()
        {
            _player.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            _player.OnFire -= this.OnFire;
        }

        private void OnFire(ShipController _)
        {
            _bulletSpawner.Spawn(
                /*_player.firePoint.position,
                _player.firePoint.up,
                _playerBulletFire.bulletSpeed,
                _playerBulletFire.bulletDamage,
                TeamType.Player*/
            );
        }
    }
}