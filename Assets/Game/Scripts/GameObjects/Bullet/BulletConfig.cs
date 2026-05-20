using UnityEngine;

namespace Game
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Game/New BulletConfig"
    )]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Speed { get; private set; }
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public Vector2 Position { get; private set; }
        [field: SerializeField] public TeamType Team { get; private set; }
        [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    }
}