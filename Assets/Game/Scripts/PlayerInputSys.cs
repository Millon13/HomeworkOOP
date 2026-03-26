using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;

public class PlayerInputSys : MonoBehaviour// сам должен получать игрока и им крутить
{
    public float dx, dy;
    [SerializeField] private Fire fire;
    [SerializeField] private PlayerShip playerShip;
    [SerializeField] private ShipController shipController;
    [SerializeField] private BulletFire bulletFire;

    public void Update()
    {
        Move();
        Fire(playerShip);
    }
    public void Move()
    {
        
        dx = Input.GetAxisRaw("Horizontal");
        dy = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector2(dx, dy);
        playerShip.Move(moveDirection);

    }
    public void Fire(PlayerShip playerShip)
    {
        if (Input.GetKeyDown(KeyCode.Space))

        { 
            fire.DoFire();
            bulletFire.Fire();
            Debug.Log("DoFire In Player");
        }
        
    }

}


