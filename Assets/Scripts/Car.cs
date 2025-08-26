using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    [Header("High-Speed Combat Racing")]
    public float maxSpeed = 40f;        // Much faster for death racing!
    public float maxReverseSpeed = 35f; // Almost as fast backward for tactical retreating
    public float acceleration = 60f;    // AGGRESSIVE acceleration for combat
    public float turnSpeed = 120f;      // Responsive turning at high speed
    public float brakeDeceleration = 50f;
    
    private Rigidbody rb;
    private float motorInput;
    private float steerInput;
    
    // Simple properties for external access
    public float Speed => rb.linearVelocity.magnitude;
    public bool IsMoving => Speed > 0.5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Lock rotation on X and Z axes - we only want Y rotation (turning)
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
        // Lower center of mass for stability (like a real car)
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }
    
    void FixedUpdate()
    {
        HandleMovement();
        HandleSteering();
        ApplyDrag();
    }
    
    // Public methods that Player.cs will call
    public void SetInputs(float motor, float steer)
    {
        motorInput = motor;
        steerInput = steer;
    }
    
    private void HandleMovement()
    {
        if (Mathf.Abs(motorInput) > 0.1f)
        {
            // Different max speeds for forward vs reverse
            float targetMaxSpeed = (motorInput > 0) ? maxSpeed : maxReverseSpeed;
            
            // Preserve existing velocity magnitude during turns
            float currentSpeed = rb.linearVelocity.magnitude;
            Vector3 targetDirection = transform.forward * motorInput;
            
            if (currentSpeed < targetMaxSpeed)
            {
                // Accelerate by adding force in the forward direction
                rb.linearVelocity += targetDirection * acceleration * Time.fixedDeltaTime;
                
                // Cap the speed but preserve direction
                if (rb.linearVelocity.magnitude > targetMaxSpeed)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * targetMaxSpeed;
                }
            }
        }
    }
    
    private void HandleSteering()
    {
        if (Mathf.Abs(steerInput) > 0.1f)
        {
            // Allow turning even when stationary - perfect for combat positioning!
            float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
            
            // Turn faster when moving, but still allow stationary turning
            if (IsMoving)
                turn *= 1.0f;  // Full speed when moving
            else
                turn *= 0.7f;  // Slightly slower when stationary, but still works
            
            transform.Rotate(0, turn, 0);
        }
    }
    
    private void ApplyDrag()
    {
        // Much gentler coasting - maintain that death race momentum!
        if (Mathf.Abs(motorInput) < 0.1f)
        {
            // Slow, gradual deceleration instead of aggressive stopping
            rb.linearVelocity *= 0.98f; // Very gentle drag - car coasts naturally
        }
    }
}