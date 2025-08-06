using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CatMeowOnAnySelection : MonoBehaviour
{
    [Header("Maullidos aleatorios")]
    public AudioClip[] meowClips;
    private AudioSource audioSource;

    [Header("XR Controller")]
    public XRBaseInteractor interactor; // Asignar la mano (ray interactor o direct interactor)

    void Start()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (interactor != null)
        {
            interactor.selectEntered.AddListener(OnSelectEnter);
        }
        else
        {
            Debug.LogWarning("No interactor assigned to CatMeowOnAnySelection.");
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        PlayRandomMeow();
    }

    void PlayRandomMeow()
    {
        if (meowClips.Length == 0) return;

        int index = Random.Range(0, meowClips.Length);
        audioSource.PlayOneShot(meowClips[index]);
    }
}
