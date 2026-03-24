
using UnityEngine;
using Game;

public class BulletMover:MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletTransform;

    private void Awake()
    {
       _bullet = GetComponent<Bullet>();
        _bulletTransform = transform;
        this.enabled = true;
        if (_bullet == null)
            Debug.LogError("Bullet component missing on " + gameObject.name);
    }
    private void Update()
     {
       Move(Time.deltaTime*200);
       Debug.Log("Move delt");
     }
    
    public void Move(float deltaTime)
    {
        Vector3 moveStep = _bullet.Direction * _bullet.Speed * deltaTime;
        Debug.Log($"Direction{_bullet.Direction},_bullet.Speed{_bullet.Speed},deltaTime{deltaTime}");
        this.transform.position += moveStep;
        _bullet.transform.rotation = Quaternion.LookRotation(_bullet.Direction, Vector3.forward);
    }
}
