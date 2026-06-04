using Game;
using UnityEngine;

public class BulletVisual : MonoBehaviour
{
    private Bullet _bullet;

    private Transform _transform;

    [SerializeField] private BulletViewConfig _configView;

    public void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    public void Initialize(BulletConfig config, TeamType team)
    {
        SetupVisual(team);

        if (_bullet != null)
            _bullet.OnHit += this.OnHit;
    }

    private void OnDestroy()
    {
        if (_bullet != null)
        {
            _bullet.OnHit -= OnHit;
        }
    }

    private void OnHit(Vector3 obj)
    {
        this.PlayExplosionVFX(transform.position);
    }

    private void SetTeamColor(TeamType team)
    {
        if (team != TeamType.None)
        {
            _configView.GeneralVFX.SetActive(true);
        }
    }

    private void SetupVisual(TeamType team)
    {
        var visual = GetComponent<BulletVisual>();
        visual?.SetTeamColor(team);
    }

    private void InstantiateVFX(Vector3 position)
    {
        Instantiate(_configView.ExplosionVFX, position, Quaternion.identity);
    }

    private void PlayExplosionVFX(Vector3 transform)
    {
        transform = _bullet.transform.position;
        InstantiateVFX(transform);
    }
}