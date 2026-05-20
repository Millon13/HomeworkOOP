using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;

public class PlayerInput : MonoBehaviour
{
    public float dx, dy;
    [SerializeField] private Ship playerShip;

    [SerializeField] private Fire PlayerFire;

    [SerializeField] private BulletConfig Config;

    [SerializeField] private BulletViewConfig ViewConfig;

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
            PlayerFire.FireTo(Config, ViewConfig, Config.Direction);
            Debug.Log("DoFire In Player");
        }
    }
}