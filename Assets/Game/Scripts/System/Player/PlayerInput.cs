using Game;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float dx, dy;
    [SerializeField] private Ship playerShip;

    [SerializeField] private FireComponent _playerFireComponent;

    [SerializeField] private BulletViewConfig ViewConfig;

    [SerializeField] private Vector2 direction = new Vector2(0, 1);

    public void Update()
    {
        Move();
        Fire();
    }

    public void Move()
    {
        dx = Input.GetAxisRaw("Horizontal");
        dy = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector2(dx, dy);
        playerShip.Move(moveDirection);
    }

    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            _playerFireComponent.FireUp();
            Debug.Log("DoFire In Player");
        }
    }
}