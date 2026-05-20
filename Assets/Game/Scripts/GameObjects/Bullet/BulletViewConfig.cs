using UnityEngine;

namespace Game
{
    // +
    [CreateAssetMenu(
        fileName = "BulletViewConfig",
        menuName = "Game/New BulletViewConfig"
    )]
    public sealed class BulletViewConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject GeneralVFX { get; private set; }

        [field: SerializeField] public GameObject ExplosionVFX { get; private set; }
    }
}