using UnityEngine;

public class HideWhenClose : MonoBehaviour
{
    public Transform target;          // Set this to your camera or XR rig
    public float hideDistance = 1f;   // How close before the object hides

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < hideDistance)
        {
            rend.enabled = false; // hides object
        }
        else
        {
            rend.enabled = true; // shows object
        }
    }
}
