using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FirefighterInteract : MonoBehaviour
{
    [Header("Postes a ocultar")]
    public GameObject[] postesAntiguos;

    [Header("Postes a activar")]
    public GameObject[] postesNuevos;

    private bool alreadyTriggered = false;

    public void ReplacePostes()
    {
        if (alreadyTriggered) return;

        // Ocultar antiguos
        foreach (GameObject poste in postesAntiguos)
        {
            if (poste != null)
                poste.SetActive(false);
        }

        // Activar nuevos
        foreach (GameObject poste in postesNuevos)
        {
            if (poste != null)
                poste.SetActive(true);
        }

        Debug.Log("🚨 Postes reemplazados por acción del bombero.");
        alreadyTriggered = true;
    }
}
