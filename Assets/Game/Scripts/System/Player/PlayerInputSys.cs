using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;

public class PlayerInputSys : MonoBehaviour
{
    public float dx, dy;
    [SerializeField] private ShipController playerShip;
    
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
            Vector2 direction = playerShip.GetFireDirection();
            Vector2 spawnPosition = PlayerFire._firePoint.position;
            PlayerFire.FireTo(Config,ViewConfig, Config.Direction);
            Debug.Log("DoFire In Player");
        }
        
    }

}


