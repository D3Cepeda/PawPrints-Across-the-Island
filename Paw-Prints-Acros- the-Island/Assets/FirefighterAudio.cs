using UnityEngine;
using UnityEngine.AI;

public class FirefighterAudio : MonoBehaviour
{
    public AudioSource arrivalAudio;  // Sonido al llegar
    public Transform destination;     // Punto al que caminar

    private NavMeshAgent agent;
    private bool hasPlayedAudio = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (destination != null && agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(destination.position);
        }
    }

    void Update()
    {
        // Evitar errores si el agente no está activo o no está en el NavMesh
        if (agent != null && agent.enabled && agent.isOnNavMesh && !hasPlayedAudio)
        {
            // Verifica si llegó
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    PlayArrivalAudio();
                    hasPlayedAudio = true;
                }
            }
        }
    }

    void PlayArrivalAudio()
    {
        if (arrivalAudio != null && !arrivalAudio.isPlaying)
        {
            arrivalAudio.Play();
            Debug.Log("🚒 Bomberos han llegado y están sonando.");
        }
    }
}
