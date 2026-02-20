using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;
public class PlayerInputSys:MonoBehaviour, IInputMovement, IInputFire
{
    public float dx, dy;
    public void InputMovement()
    {
         dx = Input.GetAxisRaw("Horizontal");
         dy = Input.GetAxisRaw("Vertical");

        

    }
    public void InputFire(ShipController shipController)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            shipController.Fire();
    }
}

public interface IInputMovement
{
    void InputMovement();
  
}
public interface IInputFire
{
   
    void InputFire(ShipController shipController);
}