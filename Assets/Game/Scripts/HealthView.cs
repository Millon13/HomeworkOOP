using UnityEngine;
 using DG.Tweening;
using Game;

public class HealthView
{
    [SerializeField] private Health _health;
    private Renderer _renderer;

    private Material _material;
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] protected ShipControllerViewConfig _viewConfig;
    [SerializeField]
    protected AudioSource _audioSource;
   
    [SerializeField]
    private AudioClip _damageSFX;
    private Tweener _damageAnimation;
    private void OnEnable()
    {

        _health.OnHealthChanged += this.OnHealthChanged;
        _health.OnDead += this.OnDead;
    }
    private void OnDisable()
    {
        _health.OnHealthChanged += this.OnHealthChanged;
        _health.OnDead -= this.OnDead;
    }

    public void OnHealthChanged(int health)
    {


    }
    public void OnDead()
    {

    }

    public void DamageSound()
    {


        if (_damageSFX)
            _audioSource.PlayOneShot(_damageSFX);
    }

   
    private void PlayAudio()
    {
        if (_damageSFX)
            _audioSource.PlayOneShot(_damageSFX);
    }
    public void Awake()
    {


        _material = new Material(_viewConfig.MaterialPrefab);
        _renderer.material = _material;
     
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

        PlayAudio();
    }
}
