using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip packagePickupClip;
    public AudioClip deliveryCompleteClip;

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayPackagePickup()
    {
        sfxSource.PlayOneShot(packagePickupClip);
    }

    public void PlayDeliveryComplete()
    {
        sfxSource.PlayOneShot(deliveryCompleteClip);
    }
}