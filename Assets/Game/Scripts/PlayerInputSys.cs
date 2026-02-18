using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;
public class PlayerInputSys:MonoBehaviour, IInput
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
            shipController.FireAction();
    }
}

public interface IInput
{
    void InputMovement();
    void InputFire(ShipController shipController);
}
