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
        [SerializeField] TeamType _teamType;
        Vector3 direction => bullet.Direction;
        Vector2 position =>bullet.transform.position;
        int damage => bullet.Damage;
        float speed => bullet.Speed;
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
            _bulletSpawner.Spawn( bullet,damage,speed, position,direction,_teamType
            
            );
       
          
            Debug.Log("OnFire In BulletInstantiator");
        }
    }
}