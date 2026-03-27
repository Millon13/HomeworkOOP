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
    [SerializeField] ParticleSystem _fireVFX;

    [Header("Visual")]
    [SerializeField]
    private Renderer _renderer;

    private Material _material;

    [SerializeField] Fire fire;
    [SerializeField] BulletSpawner spawner;
    public void Update()
    {
        fire.OnFireAnim += AnimateFire;
        fire.OnAnimDamage += AnimateDamage;
    }

    private void Fire_OnAnimDamage()
    {
        throw new System.NotImplementedException();
    }

    public void AnimateMovement(float deltaTime, Vector3 moveDirection, Transform _viewTransform, ShipControllerViewConfig _viewConfig)
    {
        Vector3 shipAngles = _viewTransform.localEulerAngles;
        shipAngles.x = _viewConfig.MoveRotationAngle * moveDirection.y;
        shipAngles.y = _viewConfig.MoveRotationAngle / 2 * moveDirection.x * -1f;

        Quaternion shipRotation = Quaternion.Euler(shipAngles);
        float t = _viewConfig.MoveSpeed * deltaTime;
        _viewTransform.localRotation = Quaternion.Lerp(_viewTransform.localRotation, shipRotation, t);
    }
    public void AnimateDamage()
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
    public void NotifyAboutHealthChanged(int health)
    {
        if (health > 0)
            AnimateDamage();

    }
    public void AnimateFire()
    {
        
        if (_fireVFX)
            _fireVFX.Play();
    }
}
