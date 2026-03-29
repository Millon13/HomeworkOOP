using UnityEngine;

public class FireView
{
    [SerializeField] private Fire _fire;
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] protected AudioClip _fireSFX;
    [SerializeField]
    protected AudioSource _audioSource;
    private void OnEnable()
    {

        _fire.OnFire += this.OnFire;
    }
    private void OnDisable()
    {
        _fire.OnFire -= this.OnFire;
    }

    public void OnFire(BulletSpawner bulletSpawner)
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

