
using UnityEngine;

public class BulletMover:MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletTransform;

    private void Awake()
    {
        //_bullet = GetComponent<Bullet>();
        _bulletTransform = transform;
        
    }
    private void Update()
     {
        Move(Time.deltaTime);
     }
    
    public void Move(float deltaTime)
    {
        Vector3 moveStep = _bullet.Direction * _bullet.Speed * deltaTime;
        Debug.Log($"Direction{_bullet.Direction},_bullet.Speed{_bullet.Speed},deltaTime{deltaTime}");
        _bulletTransform.position += moveStep;
        _bullet.transform.rotation = Quaternion.LookRotation(_bullet.Direction, Vector3.forward);
    }
}
