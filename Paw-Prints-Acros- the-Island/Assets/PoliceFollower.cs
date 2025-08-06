using UnityEngine;
using UnityEngine.AI;

public class PoliceFollower : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private bool isFollowing = false;

    [Header("Follow Settings")]
    public float followDistance = 2.0f; // Distancia detrás del jugador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isFollowing && target != null)
        {
            // Calcula la posición detrás del jugador
            Vector3 followPosition = target.position - target.forward * followDistance;
            followPosition.y = agent.transform.position.y; // mantiene altura correcta

            agent.SetDestination(followPosition);
        }
    }

    public void ActivateFollow()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }
}
