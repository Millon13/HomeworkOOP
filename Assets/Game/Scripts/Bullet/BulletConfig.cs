using UnityEngine;

public class BulletConfig
{
    [SerializeField] public  int _damage = 10;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _blueVFX;
    [SerializeField] private GameObject _redVFX;
    [SerializeField] private GameObject _explosionVFX;

    public int Damage => _damage;
    public float Speed => _speed;
    public GameObject BlueVFX => _blueVFX;
    public GameObject RedVFX => _redVFX;
    public GameObject ExplosionVFX => _explosionVFX;
}
