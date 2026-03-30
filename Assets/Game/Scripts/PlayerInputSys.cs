using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;

public class PlayerInputSys : MonoBehaviour// сам должен получать игрока и им крутить
{
    public float dx, dy;
    [SerializeField] private ShipController playerShip;
    //[SerializeField] private ShipController shipController;
    [SerializeField] private Fire PlayerFire;

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
            PlayerFire.FireTo();
            //bulletFire.Fire();
            Debug.Log("DoFire In Player");
        }
        
    }

}


