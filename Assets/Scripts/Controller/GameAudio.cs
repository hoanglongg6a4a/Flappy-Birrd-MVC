using UnityEngine;
public class GameAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flyClip, pingClip, diedClip;
    public void PlayFlapMusic()
    {
        audioSource.PlayOneShot(flyClip);
    }
    public void PlayDieMusic()
    {
        audioSource.PlayOneShot(diedClip);
    }
    public void PlayPingMusic()
    {
        audioSource.PlayOneShot(pingClip);
    }
}
