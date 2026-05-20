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

    public void Spawncooldown(float time)
    {
        if (time - _spawnTime < _spawnCooldown)
            return;
    }

    public bool IsSpawnReady()
    {
        if (Time.time - _spawnTime >= _spawnCooldown)
        {
            return true;
        }
        else
            return false;
    }

    public void ResetSpawnCooldown()
    {
        _spawnCooldown = Random.Range(cooldownConfig.MinSpawnCooldown, cooldownConfig.MaxSpawnCooldown);
        _spawnTime = Time.fixedTime;
    }
}