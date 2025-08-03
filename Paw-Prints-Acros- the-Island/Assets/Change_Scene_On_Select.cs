using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeSceneOnSelect : MonoBehaviour
{
    [Tooltip("Nombre exacto de la siguiente escena que quieres cargar.")]
    public string nextScene;

    private void OnEnable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        SceneManager.LoadScene(nextScene);
    }
}
