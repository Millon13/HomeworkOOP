using UnityEngine;

public class SpawnCooldown : MonoBehaviour
{
    [SerializeField] private CooldownConfig cooldownConfig;

    private float _spawnCooldown;

    private float _spawnTime;

    private bool _isReady;

    private void Start()
    {
        ResetSpawnCooldown();
    }

    public bool IsSpawnReady()
    {
        return Time.time - _spawnTime >= _spawnCooldown;
    }

    public void ResetSpawnCooldown()
    {
        _spawnCooldown = Random.Range(cooldownConfig.MinSpawnCooldown, cooldownConfig.MaxSpawnCooldown);
        _spawnTime = Time.fixedTime;
    }
}