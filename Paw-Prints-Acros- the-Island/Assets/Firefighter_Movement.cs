using UnityEngine;
using UnityEngine.AI;

public class FirefighterMovement : MonoBehaviour
{
    [Header("NavMesh Agent Settings")]
    public NavMeshAgent agent;

    [Header("Target to move toward")]
    public Transform destinationTarget;

    private bool isActivated = false;

    void Start()
    {
        // Desactiva el agente para que no se mueva automáticamente al iniciar
        if (agent != null)
            agent.enabled = false;
    }

    public void ActivateMovement()
    {
        if (!isActivated && agent != null && destinationTarget != null)
        {
            agent.enabled = true;
            agent.SetDestination(destinationTarget.position);
            isActivated = true;

            Debug.Log("🚒 Bomberos activados: ¡Moviéndose hacia el destino!");
        }
    }
}
