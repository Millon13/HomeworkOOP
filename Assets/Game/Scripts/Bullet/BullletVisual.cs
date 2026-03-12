using Game;
using UnityEngine;

public class BulletVisual:MonoBehaviour
{
    [SerializeField] private GameObject _blueVFX;
    [SerializeField] private GameObject _redVFX;

    public void SetTeamColor(TeamType team, BulletConfig config)
    {
        if (team == TeamType.Player)
        {
            _blueVFX.SetActive(true);
            _redVFX.SetActive(false);
        }
        else
        {
            _blueVFX.SetActive(false);
            _redVFX.SetActive(true);
        }
    }
    public void InstantiateVFX(BulletConfig config, Vector3 position)
    {
        Instantiate(config.ExplosionVFX, position, Quaternion.identity);
    }
}
