using UnityEngine;
 using DG.Tweening;
using Game;
using Modules.UI;
using Modules.Utils;

public class HealthComponentView:MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField]
    private Renderer _renderer;
    private Material _material;
    public ShipControllerSO config;

    [SerializeField]
    private Transform _viewTransform;
    [SerializeField] protected ShipControllerViewConfig _viewConfig;
    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField]
    private CameraShaker _cameraShaker;

 


    [SerializeField]
    private AudioClip _damageSFX;
    private Tweener _damageAnimation;  
    
    [Header("UI")]
    [SerializeField]
    private GameOverView _gameOverView;
    [SerializeField] ScoreView scoreView;
    [SerializeField]
    private HealthView _healthView;
    private void OnEnable()
    {

        _health.OnHealthChanged += this.OnHealthChanged;
        
        _health.OnDead += NotifyAboutDead;
        if (_gameOverView != null)
            _health.OnDead += _gameOverView.Show;
    }
    private void OnDisable()
    {
        _health.OnHealthChanged += this.OnHealthChanged;
     
        _health.OnDead -= NotifyAboutDead;

        if (_gameOverView != null)
            _health.OnDead -= _gameOverView.Show;
    }

    public void OnHealthChanged(int health)
    {
        if (_healthView != null)
            _healthView.SetHealth(health, 10);
        _cameraShaker.Shake();
        DamageSound();
        AnimateDamage();
    }

    public void NotifyAboutDead()
    {
        // Instantiate particle vfx 
        ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
        Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);

        
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
