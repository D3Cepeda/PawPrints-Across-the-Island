using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class newscript : MonoBehaviour
{
    [Header("Fish Requirements")]
    [SerializeField] private int requiredFishCount = 3; // How many fish needed to spawn
    
    [Header("Spawn Settings")]
    [SerializeField] private GameObject objectToSpawn; // What to spawn
    [SerializeField] private Transform spawnLocation; // Where to spawn (optional)
    [SerializeField] private float dropHeight = 1f; // Height above ground for floor drops
    
    [Header("Hand Spawning (Optional)")]
    [SerializeField] private XRBaseInteractor handInteractor; // XR hand to spawn to
    [SerializeField] private bool preferHandSpawn = true; // Try hand first, then floor
    
    [Header("References")]
    [SerializeField] private goneFishing fishingScript; // Reference to fishing script
    
    void Start()
    {
        // Auto-find fishing script if not assigned
        if (fishingScript == null)
        {
            fishingScript = FindObjectOfType<goneFishing>();
        }
    }

    void Update()
    {
        // Check for 'O' key press
        if (Input.GetKeyDown(KeyCode.O))
        {
            CheckFishAndSpawn();
        }
    }
    
    void CheckFishAndSpawn()
    {
        // Check if we have the required components
        if (objectToSpawn == null)
        {
            Debug.LogError("No object to spawn assigned!");
            return;
        }
        
        if (fishingScript == null)
        {
            Debug.LogError("No fishing script reference found!");
            return;
        }
        
        // Get current fish count from fishing script
        int currentFish = GetFishCount();
        Debug.Log($"Current fish count: {currentFish}, Required: {requiredFishCount}");
        
        if (currentFish >= requiredFishCount)
        {
            // Player has enough fish - spawn the object
            SpawnObject();
            
            // Optional: Reduce fish count after spawning
            // ConsumeFish();
        }
        else
        {
            Debug.Log($"Not enough fish! Need {requiredFishCount}, have {currentFish}");
        }
    }
    
    int GetFishCount()
    {
        // This assumes your goneFishing script has a public method to get fish count
        // If it doesn't, we'll need to add one or access it differently
        
        // For now, let's try to access the private field through reflection or add a public method
        // You might need to add a public method to goneFishing.cs like:
        // public int GetFishCount() { return numOfFish; }
        
        // Temporary solution - you'll need to add GetFishCount() method to goneFishing.cs
        if (fishingScript != null)
        {
            // This will work if you add a public GetFishCount() method to goneFishing.cs
            return fishingScript.GetFishCount();
        }
        
        return 0;
    }
    
    void SpawnObject()
    {
        Vector3 spawnPos;
        Quaternion spawnRot = Quaternion.identity;
        bool spawnedToHand = false;
        
        // Try to spawn to hand first if preferred and hand is available
        if (preferHandSpawn && handInteractor != null)
        {
            spawnPos = handInteractor.transform.position;
            spawnRot = handInteractor.transform.rotation;
            
            GameObject spawnedObj = Instantiate(objectToSpawn, spawnPos, spawnRot);
            
            // Try to force grab the object
            StartCoroutine(ForceGrabNextFrame(spawnedObj));
            spawnedToHand = true;
            
            Debug.Log("Spawned object to hand!");
        }
        
        // If hand spawn failed or wasn't preferred, spawn to floor
        if (!spawnedToHand)
        {
            SpawnToFloor();
        }
    }
    
    void SpawnToFloor()
    {
        Vector3 spawnPos;
        
        // Use specified spawn location or default to script position
        if (spawnLocation != null)
        {
            spawnPos = spawnLocation.position;
        }
        else
        {
            // Spawn above the script's position
            spawnPos = transform.position + Vector3.up * dropHeight;
        }
        
        GameObject spawnedObj = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
        Debug.Log("Spawned object to floor!");
        
        // Add a small random force for more natural dropping
        Rigidbody rb = spawnedObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomForce = new Vector3(
                Random.Range(-1f, 1f), 
                0, 
                Random.Range(-1f, 1f)
            ) * 2f;
            rb.AddForce(randomForce, ForceMode.Impulse);
        }
    }
    
    IEnumerator ForceGrabNextFrame(GameObject obj)
    {
        yield return null; // Wait one frame
        
        var interactable = obj.GetComponent<XRGrabInteractable>();
        if (interactable && handInteractor)
        {
            // Force the interactor to select the object
            handInteractor.interactionManager.SelectEnter(handInteractor, interactable);
            Debug.Log($"Forced grab of {obj.name}");
        }
    }
    
    // Optional method to consume fish after spawning
    void ConsumeFish()
    {
        // This would reduce the fish count - implement if needed
        // fishingScript.ConsumeFish(requiredFishCount);
    }
    
    // Public methods for external control
    public void SetRequiredFishCount(int count)
    {
        requiredFishCount = count;
    }
    
    public void SetObjectToSpawn(GameObject obj)
    {
        objectToSpawn = obj;
    }
}
