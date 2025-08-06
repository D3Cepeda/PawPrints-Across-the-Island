using UnityEngine;
using UnityEngine.AI;

public class LightPostEventTrigger : MonoBehaviour
{
    [Header("Referencias")]
    public PoliceFollower policeFollower;
    public PoliceAudio policeAudio;

    public GameObject firefighterTeam; // Bomberos (GameObject que contiene todo el equipo)
    public Transform firefighterDestination; // Empty GameObject frente al jugador o punto objetivo
    public AudioSource firefighterAudio; // Sirena o voz al llegar

    private bool eventTriggered = false;
    private NavMeshAgent firefighterAgent;
    private bool firefighterSoundPlayed = false;

    void Update()
    {
        // Revisar si los bomberos ya llegaron y deben sonar
        if (firefighterAgent != null && !firefighterSoundPlayed)
        {
            if (firefighterAgent.isOnNavMesh && !firefighterAgent.pathPending && firefighterAgent.remainingDistance <= firefighterAgent.stoppingDistance)
            {
                if (!firefighterAgent.hasPath || firefighterAgent.velocity.sqrMagnitude == 0f)
                {
                    firefighterAudio?.Play();
                    firefighterSoundPlayed = true;
                    Debug.Log("🔥 ¡Bomberos llegaron y están sonando!");
                }
            }
        }
    }

    public void TriggerEmergencyEvent()
    {
        if (eventTriggered) return;

        // Detener policía
        policeFollower?.StopFollowing();
        policeAudio?.PlayStopVoice();

        // Activar bomberos
        if (firefighterTeam != null)
        {
            firefighterTeam.SetActive(true);
            firefighterAgent = firefighterTeam.GetComponent<NavMeshAgent>();

            if (firefighterAgent != null && firefighterDestination != null)
            {
                if (firefighterAgent.isActiveAndEnabled && firefighterAgent.isOnNavMesh)
                {
                    firefighterAgent.SetDestination(firefighterDestination.position);
                    Debug.Log("🛑 Bomberos activados: ¡Moviéndose hacia el destino!");
                }
                else
                {
                    Debug.LogWarning("⚠️ El agente de bomberos no está en una NavMesh activa o no está habilitado.");
                }
            }
        }

        eventTriggered = true;
        Debug.Log("🚨 Emergencia activada: Policía detenida, bomberos en camino.");
    }
}
