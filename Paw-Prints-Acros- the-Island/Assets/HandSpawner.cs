using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class HandSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectPrefab;                // Prefab to spawn
    public Transform spawnLocation;                // Where to spawn (optional, uses this transform if null)
    public int numberOfSpawns = 5;                 // Total number to spawn
    public float delayBetweenSpawns = 2f;          // Seconds between spawns
    
    [Header("XR Settings (Optional)")]
    public XRBaseInteractor handInteractor;        // The XR hand to receive the item (optional)
    public bool forceGrabOnSpawn = false;          // Whether to force grab spawned objects
    
    [Header("Spawning Control")]
    public bool spawnOnStart = false;              // Whether to start spawning immediately
    public bool allowMultipleTriggers = true;     // Allow triggering multiple spawn cycles
    
    private int spawnCount = 0;
    private bool isSpawning = false;
    private Coroutine spawnCoroutine;

    void Start()
    {
        if (spawnOnStart)
        {
            TriggerSpawning();
        }
    }

    // Public method to trigger spawning from external scripts
    public void TriggerSpawning()
    {
        if (isSpawning && !allowMultipleTriggers)
        {
            Debug.Log("Spawning already in progress and multiple triggers not allowed.");
            return;
        }
        
        if (objectPrefab == null)
        {
            Debug.LogError("No object prefab assigned to HandSpawner!");
            return;
        }

        // Stop any existing spawning coroutine if multiple triggers are allowed
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        
        // Reset spawn count for new cycle
        spawnCount = 0;
        spawnCoroutine = StartCoroutine(SpawnRepeatedly());
        Debug.Log($"Started spawning {numberOfSpawns} objects with {delayBetweenSpawns}s delay");
    }

    // Public method to stop spawning
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        isSpawning = false;
        Debug.Log("Spawning stopped");
    }

    IEnumerator SpawnRepeatedly()
    {
        isSpawning = true;
        
        while (spawnCount < numberOfSpawns)
        {
            SpawnObject();
            spawnCount++;
            
            // Don't wait after the last spawn
            if (spawnCount < numberOfSpawns)
            {
                yield return new WaitForSeconds(delayBetweenSpawns);
            }
        }
        
        isSpawning = false;
        spawnCoroutine = null;
        Debug.Log($"Finished spawning {spawnCount} objects");
    }

    void SpawnObject()
    {
        // Determine spawn position and rotation
        Vector3 spawnPos = spawnLocation != null ? spawnLocation.position : transform.position;
        Quaternion spawnRot = spawnLocation != null ? spawnLocation.rotation : transform.rotation;
        
        // If hand interactor is assigned and no spawn location, use hand position
        if (handInteractor != null && spawnLocation == null)
        {
            spawnPos = handInteractor.transform.position;
            spawnRot = handInteractor.transform.rotation;
        }

        GameObject newObj = Instantiate(objectPrefab, spawnPos, spawnRot);
        Debug.Log($"Spawned object {spawnCount + 1}/{numberOfSpawns}: {newObj.name}");

        // Force grab if XR hand is assigned and option is enabled
        if (forceGrabOnSpawn && handInteractor != null)
        {
            StartCoroutine(ForceGrabNextFrame(newObj));
        }
    }

    IEnumerator ForceGrabNextFrame(GameObject obj)
    {
        yield return null; // Wait for physics to settle

        var interactable = obj.GetComponent<XRGrabInteractable>();
        if (interactable && handInteractor)
        {
            // Force the interactor to select the object
            handInteractor.interactionManager.SelectEnter(handInteractor, interactable);
            Debug.Log($"Forced grab of {obj.name}");
        }
    }
    
    // Public methods for external control
    public bool IsSpawning() => isSpawning;
    public int GetSpawnCount() => spawnCount;
    public int GetRemainingSpawns() => numberOfSpawns - spawnCount;
    
    // Method to set spawn parameters at runtime
    public void SetSpawnParameters(int count, float delay)
    {
        numberOfSpawns = count;
        delayBetweenSpawns = delay;
    }
}
