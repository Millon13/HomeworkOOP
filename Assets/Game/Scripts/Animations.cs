using DG.Tweening;
using Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Animations:MonoBehaviour
{
   
    [SerializeField]
    private Audio _audio;

    private ShipControllerViewConfig _viewConfig;

    private Tweener _damageAnimation;

    [Header("Visual")]
    [SerializeField]
    private Renderer _renderer;

    private Material _material;

  
    public void AnimateMovement(float deltaTime, Vector3 moveDirection, Transform _viewTransform, ShipControllerViewConfig _viewConfig)
    {
        Vector3 shipAngles = _viewTransform.localEulerAngles;
        shipAngles.x = _viewConfig.MoveRotationAngle * moveDirection.y;
        shipAngles.y = _viewConfig.MoveRotationAngle / 2 * moveDirection.x * -1f;

        Quaternion shipRotation = Quaternion.Euler(shipAngles);
        float t = _viewConfig.MoveSpeed * deltaTime;
        _viewTransform.localRotation = Quaternion.Lerp(_viewTransform.localRotation, shipRotation, t);
    }
    public void AnimateDamage(ShipControllerViewConfig _viewConfig)
    {
        if (_damageAnimation.IsActive())
            _damageAnimation.Kill();

        _damageAnimation = DOVirtual.Float(
            0f,
            1f,
            _viewConfig.HitDuration,
            progress => _material?.SetFloat(_viewConfig.HitPropertyName,
                _viewConfig.HitAnimationCurve.Evaluate(progress))
        ).SetLink(_renderer.gameObject);

        //if (_damageSFX)
            _audio.DamageSound();
    }
    public void AnimateAwake(ShipControllerViewConfig _viewConfig)
    {
       

        _material = new Material(_viewConfig.MaterialPrefab);
        _renderer.material = _material;
    }
    public void VFXIntitiator(ParticleSystem prefab,ShipControllerViewConfig _viewConfig)
    {

        // Instantiate particle vfx 
        prefab = _viewConfig.DestroyEffectPrefab;
       
    }
}
