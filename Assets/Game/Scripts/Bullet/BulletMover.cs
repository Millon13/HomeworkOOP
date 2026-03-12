
using UnityEngine;

public class BulletMover:MonoBehaviour
{
    private Bullet _bullet;
    private Transform _transform;

    private void Awake()
    {
        _bullet = GetComponent<Bullet>();
        _transform = transform;
    }

    public void Move(float deltaTime)
    {
        Vector3 moveStep = _bullet.Direction * _bullet.Speed * deltaTime;
        _transform.position += moveStep;
    }
}
