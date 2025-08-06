using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PoliceAudio : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;

    [Header("Dependencies")]
    public PoliceFollower policeFollower; // <- Asegúrate de arrastrarlo en el Inspector

    public void PlayVoiceAndFollow()
    {
        Debug.Log("Policía activada: hablando y comenzando a seguir.");

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (policeFollower != null)
        {
            policeFollower.ActivateFollow();
        }
    }
}
