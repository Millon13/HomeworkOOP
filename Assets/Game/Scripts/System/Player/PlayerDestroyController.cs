using UnityEngine;

public class PlayerDestroyController : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;

    [SerializeField] private GameObject _player;

    private void OnEnable()
    {
        _healthComponent.OnDead += PlayerVanished;
    }

    private void OnDisable()
    {
        _healthComponent.OnDead -= PlayerVanished;
    }

    private void PlayerVanished()
    {
        Destroy(_player.gameObject);
    }
}