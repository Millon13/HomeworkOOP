using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerBulletInstantiator : MonoBehaviour
    {
        [SerializeField]
        private BulletWorldGO _bulletWorld;

        [SerializeField]
        private PlayerShip _player;

        [SerializeField]
        private BulletFire _playerBulletFire;
        private void OnEnable()
        {
            _playerBulletFire.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            _playerBulletFire.OnFire -= this.OnFire;
        }

        private void OnFire(ShipController _)
        {
            _bulletWorld.Spawn(
                _playerBulletFire.firePoint.position,
                _playerBulletFire.firePoint.up,
                _playerBulletFire.bulletSpeed,
                _playerBulletFire.bulletDamage,
                TeamType.Player
            );
        }
    }
}