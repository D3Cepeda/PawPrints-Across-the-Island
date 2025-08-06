using UnityEngine;
using UnityEngine.InputSystem;

public class CatClickRandomSound : MonoBehaviour
{
    [Header("Lista de maullidos")]
    public AudioClip[] meowClips; // Los diferentes sonidos
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Detectar click o trigger (ideal para pruebas y VR)
        if (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayRandomMeow();
        }
    }

    void PlayRandomMeow()
    {
        if (meowClips.Length == 0) return;

        int index = Random.Range(0, meowClips.Length);
        AudioClip selectedMeow = meowClips[index];

        audioSource.clip = selectedMeow;
        audioSource.Play();
    }
}
