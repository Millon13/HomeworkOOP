using Game;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Ship playerShip;

    [SerializeField] private Vector2 direction = new Vector2(0, 1);

    public void Update()
    {
        Move();
        Fire();
    }

    public void Move()
    {
        var dx = Input.GetAxisRaw("Horizontal");
        var dy = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector2(dx, dy);
        playerShip.Move(moveDirection);
    }

    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            playerShip.FireUp();
        }
    }
}