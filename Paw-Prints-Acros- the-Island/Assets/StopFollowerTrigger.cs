using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StopFollowerTrigger : MonoBehaviour
{
    public PoliceFollower policeFollower;

    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelected);
        }
    }

    void OnSelected(SelectEnterEventArgs args)
    {
        policeFollower.StopFollowing();
    }
}
