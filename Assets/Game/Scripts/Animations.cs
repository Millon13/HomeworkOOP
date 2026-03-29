using DG.Tweening;
using Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Animations : MonoBehaviour
{

    [SerializeField]
    private Audio _audio;

    private ShipControllerViewConfig _viewConfig;

    //private Tweener _damageAnimation;
    //  [SerializeField] ParticleSystem _fireVFX;

    [Header("Visual")]
    [SerializeField]
    private Renderer _renderer;

    private Material _material;

    [SerializeField] Fire fire;
    [SerializeField] BulletSpawner spawner;
    public void Update()
    {
        //fire.OnFireAnim += AnimateFire;
       // fire.OnAnimDamage += AnimateDamage;
    }

    private void Fire_OnAnimDamage()
    {
        throw new System.NotImplementedException();
    }



    public void AnimateAwake(ShipControllerViewConfig _viewConfig)
    {


        _material = new Material(_viewConfig.MaterialPrefab);
        _renderer.material = _material;
    }
    public void VFXIntitiator(ParticleSystem prefab, ShipControllerViewConfig _viewConfig)
    {

        // Instantiate particle vfx 
        prefab = _viewConfig.DestroyEffectPrefab;

    }
    public void NotifyAboutHealthChanged(int health)
    {
        //if (health > 0)
         //   AnimateDamage();

    }
}
   
