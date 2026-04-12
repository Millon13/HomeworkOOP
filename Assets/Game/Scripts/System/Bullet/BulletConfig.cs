using UnityEngine;

public class BulletConfig:ScriptableObject
{
       [SerializeField]public int Damage { get; private set; }
       [SerializeField]public int Speed { get; private set; }
}
