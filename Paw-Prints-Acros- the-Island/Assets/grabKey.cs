using UnityEngine;

public class grabKey : MonoBehaviour
{
    [Header("Spin Settings")]
    [SerializeField] private Vector3 spinSpeed = new Vector3(0, 50, 0); // Degrees per second
    [SerializeField] private bool spinOnStart = true;
    [SerializeField] private Space rotationSpace = Space.Self;
    
    private bool isSpinning = false;
    
    void Start()
    {
        if (spinOnStart)
        {
            StartSpinning();
        }
    }
    
    void Update()
    {
        if (isSpinning)
        {
            // Rotate the object based on spin speed and time
            transform.Rotate(spinSpeed * Time.deltaTime, rotationSpace);
        }
    }
    
    // Public methods to control spinning
    public void StartSpinning()
    {
        isSpinning = true;
        Debug.Log($"{gameObject.name} started spinning");
    }
    
    public void StopSpinning()
    {
        isSpinning = false;
        Debug.Log($"{gameObject.name} stopped spinning");
    }
    
    public void ToggleSpinning()
    {
        if (isSpinning)
            StopSpinning();
        else
            StartSpinning();
    }
    
    // Method to change spin speed at runtime
    public void SetSpinSpeed(Vector3 newSpeed)
    {
        spinSpeed = newSpeed;
    }
    
    // Method to set spin speed with individual axes
    public void SetSpinSpeed(float x, float y, float z)
    {
        spinSpeed = new Vector3(x, y, z);
    }
}
