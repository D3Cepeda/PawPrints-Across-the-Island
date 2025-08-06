using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ConfigureSettingsOnStart : MonoBehaviour
{
    public MonoBehaviour snapTurn;
    public MonoBehaviour continuousTurn;
    public MonoBehaviour continuousMove;
    public MonoBehaviour distanceGrab;

    void Start()
    {
        if (UserSettingsManager.Instance == null)
        {
            Debug.LogWarning("No se encontró UserSettingsManager.");
            return;
        }

        if (snapTurn != null)
            snapTurn.enabled = UserSettingsManager.Instance.useSnapTurn;

        if (continuousTurn != null)
            continuousTurn.enabled = UserSettingsManager.Instance.useContinuousTurn;

        if (continuousMove != null)
            continuousMove.enabled = UserSettingsManager.Instance.useContinuousMove;

        if (distanceGrab != null)
            distanceGrab.enabled = UserSettingsManager.Instance.useDistanceGrab;

        Debug.Log("Configuraciones aplicadas en la escena Beach.");
    }
}
