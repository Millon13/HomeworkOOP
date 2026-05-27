using UnityEngine;

public class FireComponentView : MonoBehaviour
{
    [SerializeField] private FireComponent _fireComponent;
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] protected AudioClip _fireSFX;
    [SerializeField] protected AudioSource _audioSource;

    private void OnEnable()
    {
        _fireComponent.OnFire += this.OnFireComponent;
    }

    private void OnDisable()
    {
        _fireComponent.OnFire -= this.OnFireComponent;
    }

    public void OnFireComponent()
    {
        PlayAudio();
        AnimateFire();
    }

    private void PlayAudio()
    {
        if (_fireSFX)
            _audioSource.PlayOneShot(_fireSFX);
    }

    public void AnimateFire()
    {
        if (_fireVFX)
            _fireVFX.Play();
    }
}