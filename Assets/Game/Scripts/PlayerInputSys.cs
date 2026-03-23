using Game;
using UnityEngine;
using Modules.UI;
using Modules.Utils;

public class PlayerInputSys : MonoBehaviour, IShipMove// сам должен получать игрока и им крутить
{
    public float dx, dy;
    [SerializeField] private Fire fire;
    [SerializeField] private BulletFire bulletFire;
    public void Move()
    {
        dx = Input.GetAxisRaw("Horizontal");
        dy = Input.GetAxisRaw("Vertical");



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


