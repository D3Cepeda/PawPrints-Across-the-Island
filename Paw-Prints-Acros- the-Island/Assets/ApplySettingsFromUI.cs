using UnityEngine;
using UnityEngine.UI;

public class ApplySettingsFromUI : MonoBehaviour
{
    public Toggle snapTurnToggle;
    public Toggle continuousTurnToggle;
    public Toggle continuousMoveToggle;
    public Toggle distanceGrabToggle;

    public void SaveUserSettings()
    {
        if (UserSettingsManager.Instance == null) return;

        UserSettingsManager.Instance.useSnapTurn = snapTurnToggle.isOn;
        UserSettingsManager.Instance.useContinuousTurn = continuousTurnToggle.isOn;
        UserSettingsManager.Instance.useContinuousMove = continuousMoveToggle.isOn;
        UserSettingsManager.Instance.useDistanceGrab = distanceGrabToggle.isOn;

        Debug.Log("Opciones guardadas!");
    }
}
