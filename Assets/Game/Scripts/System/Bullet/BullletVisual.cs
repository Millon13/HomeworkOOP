using Game;
using System;
using UnityEngine;

public class BulletVisual:MonoBehaviour
{
    private Bullet _bullet;

    private Transform _transform;

    [SerializeField] private GameObject _blueVFX;

    [SerializeField] private GameObject _redVFX;

    [SerializeField] private GameObject _explosionVFX;

    public void Awake()
    {
        _bullet = GetComponent<Bullet>();
    
    }
    private void OnEnable()
    {
        _bullet.OnHit += this.OnHit;

    }
    private void OnDisable()
    {
        _bullet.OnHit -= this.OnHit;
    }
    private void OnHit(Vector3 obj)
    {
        this.PlayExplosionVFX(transform.position);
    }
    public void SetTeamColor(TeamType team)
    {
      
        if (team == TeamType.Player)
        {
            _blueVFX.SetActive(true);
            _redVFX.SetActive(false);
        }
        else if (team == TeamType.Enemy) 
        {
            _blueVFX.SetActive(false);
            _redVFX.SetActive(true);
        }
    }
    public void InstantiateVFX( Vector3 position)
    {
        Instantiate(_explosionVFX, position, Quaternion.identity);
    }
    public void PlayExplosionVFX( Vector3 transform)
    {
        transform = _bullet.transform.position;
        InstantiateVFX(transform);
    }
}
