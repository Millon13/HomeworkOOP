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
        //_bullet.OnHandleHit += PlayExplosionVFX;


        /*if(_bullet.enabled)
        {
            SetTeamColor(_bullet.Team);
        }*/
    }
    public void Update()
    {
        
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
        PlayExplosionVFX(transform);
    }
}
