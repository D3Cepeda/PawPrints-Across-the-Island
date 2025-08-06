using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PoliceAudio : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip followClip;
    public AudioClip stopClip;

    [Header("Dependencies")]
    public PoliceFollower policeFollower;

    [Header("Tablet Video")]
    public PlayVideo tabletVideoPlayer;
    public int videoIndexToPlay = 1; // Cambia según el orden de tus clips
    private bool hasPlayedTabletVideo = false;

    public void PlayVoiceAndFollow()
    {
        Debug.Log("Policía activada: hablando y comenzando a seguir.");

        if (audioSource != null && followClip != null)
        {
            audioSource.clip = followClip;
            audioSource.Play();
        }

        if (policeFollower != null)
        {
            policeFollower.ActivateFollow();
        }

        if (!hasPlayedTabletVideo && tabletVideoPlayer != null)
        {
            tabletVideoPlayer.PlayAtIndex(videoIndexToPlay);
            hasPlayedTabletVideo = true;
        }
    }

    public void PlayStopVoice()
    {
        if (audioSource != null && stopClip != null)
        {
            audioSource.clip = stopClip;
            audioSource.Play();
        }

        if (policeFollower != null)
        {
            policeFollower.StopFollowing();
        }
    }
}
