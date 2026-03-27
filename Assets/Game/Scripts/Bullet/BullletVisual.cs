using Game;
using System;
using UnityEngine;

public class BulletVisual:MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject _blueVFX;
    [SerializeField] private GameObject _redVFX;
    [SerializeField] private GameObject _explosionVFX;
    public void Awake()
    {
        bullet.OnHandleHit += PlayExplosionVFX;

        if(bullet.enabled)
        {
            SetTeamColor(bullet.Team);
        }
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
        PlayExplosionVFX(bullet.transform.position);
    }
}
