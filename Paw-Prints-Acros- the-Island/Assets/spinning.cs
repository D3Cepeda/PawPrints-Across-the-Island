using UnityEngine;

public class spinning : MonoBehaviour
{
    [Header("Spinning Settings")]
    [SerializeField] private Vector3 spinSpeed = new Vector3(0, 50, 0); // Degrees per second
    [SerializeField] private bool enableSpinning = true;
    
    [Header("Bobbing Settings")]
    [SerializeField] private float bobSpeed = 2f; // How fast it bobs
    [SerializeField] private float bobHeight = 0.5f; // How high/low it bobs
    [SerializeField] private bool enableBobbing = true;
    
    [Header("Control")]
    [SerializeField] private bool startOnAwake = true;
    
    private Vector3 startPosition;
    private bool isAnimating = false;
    private float bobTimer = 0f;
    
    void Start()
    {
        // Store the starting position for bobbing
        startPosition = transform.position;
        
        if (startOnAwake)
        {
            StartAnimation();
        }
    }
    
    void Update()
    {
        if (!isAnimating) return;
        
        // Handle spinning
        if (enableSpinning)
        {
            transform.Rotate(spinSpeed * Time.deltaTime, Space.Self);
        }
        
        // Handle bobbing
        if (enableBobbing)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            
            // Calculate new Y position using sine wave for smooth bobbing
            float newY = startPosition.y + Mathf.Sin(bobTimer) * bobHeight;
            
            // Apply the new position while keeping X and Z the same
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
    
    // Public methods to control the animation
    public void StartAnimation()
    {
        isAnimating = true;
        Debug.Log($"{gameObject.name} started spinning and bobbing");
    }
    
    public void StopAnimation()
    {
        isAnimating = false;
        Debug.Log($"{gameObject.name} stopped spinning and bobbing");
    }
    
    public void ToggleAnimation()
    {
        if (isAnimating)
            StopAnimation();
        else
            StartAnimation();
    }
    
    // Methods to control individual effects
    public void ToggleSpinning()
    {
        enableSpinning = !enableSpinning;
        Debug.Log($"Spinning: {enableSpinning}");
    }
    
    public void ToggleBobbing()
    {
        enableBobbing = !enableBobbing;
        Debug.Log($"Bobbing: {enableBobbing}");
    }
    
    // Methods to adjust settings at runtime
    public void SetSpinSpeed(Vector3 newSpeed)
    {
        spinSpeed = newSpeed;
    }
    
    public void SetSpinSpeed(float x, float y, float z)
    {
        spinSpeed = new Vector3(x, y, z);
    }
    
    public void SetBobSettings(float speed, float height)
    {
        bobSpeed = speed;
        bobHeight = height;
    }
    
    // Reset position to original
    public void ResetPosition()
    {
        transform.position = startPosition;
        bobTimer = 0f;
    }
    
    // Public getters
    public bool IsAnimating() => isAnimating;
    public bool IsSpinning() => enableSpinning;
    public bool IsBobbing() => enableBobbing;
}
