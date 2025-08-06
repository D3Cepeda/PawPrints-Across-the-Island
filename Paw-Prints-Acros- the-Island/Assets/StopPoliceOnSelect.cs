using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StopPoliceOnSelect : MonoBehaviour
{
    [Header("Asignar el seguidor policial")]
    public PoliceFollower policeFollower;

    [Header("Este interactor (mano con rayo o directo)")]
    public XRBaseInteractor interactor;

    void Start()
    {
        if (interactor != null)
        {
            interactor.selectEntered.AddListener(OnSelectEnter);
        }
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (policeFollower != null)
        {
            policeFollower.StopFollowing();
            Debug.Log("La policía ha detenido su persecución.");
        }
    }
}
