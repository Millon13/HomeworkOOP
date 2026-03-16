using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerBulletInstantiator : MonoBehaviour
    {
        [SerializeField]
        private PlayerShip _player;

        [SerializeField]
        private BulletFire _playerBulletFire;

        //[SerializeField]
        //private BulletSpawner _bulletSpawner;
        private void OnEnable()
        {
            _player.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            _player.OnFire -= this.OnFire;
        }

        private void OnFire(BulletSpawner _bulletSpawner)
        {
            _bulletSpawner.Spawn(
            // параметры...
            );
        }
    }
}