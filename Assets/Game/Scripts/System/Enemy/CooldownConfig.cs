using UnityEngine;

[CreateAssetMenu(fileName = "CooldownConfig", menuName = "Scriptable Objects/CooldownConfig")]
public class CooldownConfig : ScriptableObject
{
    [field: SerializeField] public float MinSpawnCooldown { get; private set; }
    [field: SerializeField] public float MaxSpawnCooldown { get; private set; }
    
}
