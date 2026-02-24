using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;
public class PlayerInputSys:MonoBehaviour, IShipMove, IShipFire
{
    public float dx, dy;
    public void Move()
    {
         dx = Input.GetAxisRaw("Horizontal");
         dy = Input.GetAxisRaw("Vertical");

        

    }
    public void Fire(ShipController shipController)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            shipController.Fire(shipController);
    }

}


