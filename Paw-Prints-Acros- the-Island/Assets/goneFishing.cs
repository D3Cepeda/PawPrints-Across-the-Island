using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class goneFishing : MonoBehaviour
{
    
    [Header("Fishing Settings")]
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 5f;
    [SerializeField] private float fishToCatch = 3;
    [SerializeField] private float fishCatchChance = 0.3f; // 30% chance
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fishingSound;
    [SerializeField] private AudioClip winningSound;
    
    [Header("UI")]
    [SerializeField] private GameObject messageUI;
    [SerializeField] private TMPro.TextMeshProUGUI messageText;
    
    [Header("Key System")]
    [SerializeField] private grabKey keyObject; // Reference to the key that should appear
    
    private bool isHoldingObject = false;
    private bool isFishing = false;
    private bool canFish = false;


    private int numOfFish = 0;
    public TEST1 listener;

    // Messages for different outcomes
    private string[] successMessages = {
        "You caught a fish! 🐟",
        "Great catch! A nice fish! 🎣",
        "Fish on the line! Well done! 🐠"
    };
    
    private string[] failureMessages = {
        "No fish this time... 🎣",
        "The fish got away! 😅",
        "Better luck next time! 🐟"
    };
    
    void Start()
    {
        // Initialize audio source if not assigned\
        Debug.Log("goneFishing script started");
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // Hide message UI initially
        if (messageUI != null)
        {
            messageUI.SetActive(false);
        }
    }
    
    void Update()
    {
        // Check for 'C' key press when holding object
        Debug.Log("Update called "  + isHoldingObject + " " + isFishing);
        if (Input.GetKeyDown(KeyCode.C) && isHoldingObject && !isFishing)
        {
            StartFishing();
        }
    }
    
    // Called when object is picked up (attach this to your pickup system)
    public void OnGrabbed()
    {
        isHoldingObject = true;
        canFish = true;
        Debug.Log("Object picked up - ready to fish!");
    }
    
    // Called when object is dropped (attach this to your pickup system)
    public void OnObjectDropped()
    {
        isHoldingObject = false;
        canFish = false;
        isFishing = false;
        Debug.Log("Object dropped - fishing stopped.");
    }
    
    private void StartFishing()
    {
        Debug.Log("StartFishing called");
        if (!canFish || isFishing) return;
        
        isFishing = true;
        Debug.Log("Starting to fish...");
        
        // Play fishing sound
        if (audioSource != null && fishingSound != null)
        {
            audioSource.PlayOneShot(fishingSound);
        }
        
        // Start the fishing coroutine
        StartCoroutine(FishingCoroutine());
    }
    
    private IEnumerator FishingCoroutine()
    {
        // Wait for random time between min and max
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        Debug.Log($"Fishing for {waitTime:F1} seconds...");
        
        yield return new WaitForSeconds(waitTime);
        
        // Determine if fish was caught
        bool caughtFish = Random.Range(0f, 1f) < fishCatchChance;
        
        // Show result message
        ShowFishingResult(caughtFish);
        
        // Reset fishing state
        isFishing = false;
    }
    
    private void ShowFishingResult(bool caughtFish)
    {
        string message;
        
        if (caughtFish)
        {
            message = successMessages[Random.Range(0, successMessages.Length)];
            Debug.Log("SUCCESS: " + message);

            if (audioSource != null && winningSound != null)
            {
                audioSource.PlayOneShot(winningSound);
            }

            listener.upadteAmount();
            numOfFish++;

            Debug.Log("numOfFish: " + numOfFish);


            if(numOfFish > fishToCatch){
                message = "You've caught " + fishToCatch + " fish! You won the game!";
                Debug.Log(message);
                if (audioSource != null && winningSound != null)
                {
                    audioSource.PlayOneShot(winningSound);
                }
                
                isFishing = false;

                // Make the key appear and start spinning when player wins
                if (keyObject != null)
                {
                    keyObject.gameObject.SetActive(true);
                    keyObject.StartSpinning();
                    Debug.Log("Key appeared and started spinning!");
                }

                numOfFish = 0;
            }
        }
        else
        {
            message = failureMessages[Random.Range(0, failureMessages.Length)];
            Debug.Log("FAILED: " + message);
        }
        
        // Display message in UI
        if (messageUI != null && messageText != null)
        {
            messageText.text = message;
            messageUI.SetActive(true);
            
            // Hide message after 3 seconds
            StartCoroutine(HideMessageAfterDelay(3f));
        }
    }
    
    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (messageUI != null)
        {
            messageUI.SetActive(false);
        }
    }
    
    // Public methods for external systems to call
    public bool IsHoldingObject() => isHoldingObject;
    public bool IsFishing() => isFishing;
    public bool CanFish() => canFish;
    public int GetFishCount() => numOfFish;
    
    // Method to set fishing sound at runtime
    public void SetFishingSound(AudioClip newSound)
    {
        fishingSound = newSound;
    }
    
    // Method to adjust fish catch chance
    public void SetFishCatchChance(float newChance)
    {
        fishCatchChance = Mathf.Clamp01(newChance);
    }
}


// using UnityEngine;
// using System.Collections;
// using UnityEngine.XR.Interaction.Toolkit;


// public class goneFishing : MonoBehaviour
// {


//     IEnumerator WaitAndDoSomething()
//     {
//         Debug.Log("Waiting...");
//         yield return new WaitForSeconds(2f); // wait 2 seconds
//         Debug.Log("Done waiting!");
//     }
//     private XRGrabInteractable grabInteractable;
//     private bool isBeingHeld = false;

//     void Awake()
//     {
//         grabInteractable = GetComponent<XRGrabInteractable>();
//     }

//     void OnEnable()
//     {
//         grabInteractable.selectEntered.AddListener(OnGrab);
//         grabInteractable.selectExited.AddListener(OnRelease);
//     }

//     void OnDisable()
//     {
//         grabInteractable.selectEntered.RemoveListener(OnGrab);
//         grabInteractable.selectExited.RemoveListener(OnRelease);
//     }

//     void OnGrab(SelectEnterEventArgs args)
//     {
//         isBeingHeld = true;
//     }

//     void OnRelease(SelectExitEventArgs args)
//     {
//         isBeingHeld = false;
//     }

//     void Update()
//     {
//         if (isBeingHeld)
//         {
//             Debug.Log("Object is being held!");

//             if (Input.GetKey(KeyCode.C))
//             {

//                 int waitTime = Random.Range(1, 6);

//                 StartCoroutine(WaitAndDoSomething());

//                 transform.rotation = Quaternion.Slerp(
//                     transform.rotation,
//                     Quaternion.Euler(0, 90, 0),
//                     Time.deltaTime * 2f
//                 );

//                 Debug.Log("Casting");
//             }
//         }
//     }
// }
