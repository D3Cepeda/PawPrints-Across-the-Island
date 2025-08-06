using UnityEngine;
using UnityEngine.AI;

public class PoliceFollower : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private bool isFollowing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isFollowing && target != null)
        {
            agent.SetDestination(target.position);
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
