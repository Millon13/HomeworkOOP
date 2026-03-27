using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Audio:MonoBehaviour
{
    [SerializeField]
    protected AudioClip _fireSFX;

    [SerializeField]
    private AudioClip _damageSFX;

    [SerializeField]
    protected AudioSource _audioSource;

    public void DamageSound()
    {
       

        if (_damageSFX)
            _audioSource.PlayOneShot(_damageSFX);
    }
    
    public void FireSound()
    {
        if (_fireSFX)
            _audioSource.PlayOneShot(_fireSFX);
    }
}
