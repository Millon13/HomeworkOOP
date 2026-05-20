using UnityEngine;

namespace Game
{
    [CreateAssetMenu(
        fileName = "BulletEnemyConfig",
        menuName = "Game/New BulletEnemyConfig"
    )]
    public class BulletConfigEnemy : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Speed { get; private set; }
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public int Position { get; private set; }
    }
}