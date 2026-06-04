using UnityEngine;
using DG.Tweening;
using Game;
using Modules.UI;

public class HealthComponentView : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;

    [SerializeField] private Renderer _renderer;

    private Material _material;

    [SerializeField] private Transform _viewTransform;

    [SerializeField] protected ShipViewConfig _viewConfig;

    [SerializeField] protected AudioSource _audioSource;

    [SerializeField] private AudioClip _damageSFX;

    private Tweener _damageAnimation;

    [SerializeField] private HealthView _healthView;

    private void OnEnable()
    {
        _healthComponent.OnHealthChanged += this.OnHealthComponentChanged;
        _healthComponent.OnDead += this.NotifyAboutDead;
    }

    private void OnDisable()
    {
        _healthComponent.OnHealthChanged -= this.OnHealthComponentChanged;

        _healthComponent.OnDead -= this.NotifyAboutDead;
    }

    private void OnHealthComponentChanged(int health)
    {
        if (_healthView != null)
            _healthView.SetHealth(health, 10);

        DamageSound();
        AnimateDamage();
    }

    private void NotifyAboutDead()
    {
        ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
        Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);
    }

    private void DamageSound()
    {
        if (_damageSFX)
            _audioSource.PlayOneShot(_damageSFX);
    }


    private void PlayAudio()
    {
        if (_damageSFX)
            _audioSource.PlayOneShot(_damageSFX);
    }

    private void Awake()
    {
        _material = new Material(_viewConfig.MaterialPrefab);
        _renderer.material = _material;
    }

    private void AnimateDamage()
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
        PlayAudio();
    }
}