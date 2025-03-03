using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float jumpForce;
    [SerializeField] private float extraGravity;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 movementInput;
    private Rigidbody rb;
    private float initialSpeed;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ToggleCursor.Toggle();
        initialSpeed = speed;
    }
    
    public void OnMove(InputValue value) // Callback function from Input System
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        speed = value.isPressed ? runningSpeed : initialSpeed;
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Update()
    {
        CheckGround();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Compute movement direction
        Vector3 moveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

        // Apply movement using Rigidbody
        if (rb.linearVelocity.y < -0.1f && !isGrounded)
        {
            Debug.Log("Falling");
            Debug.Log(rb.linearVelocity.y);
            rb.linearVelocity += extraGravity * Time.deltaTime * Vector3.down;
        }
        else
        {
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
    }

    // We're using raycast to efficiently check if the player is on the ground.
    // Feel free to change the value of `groundCheckDistance`.
    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // This is the red line below the player.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.down * groundCheckDistance;
        Gizmos.DrawLine(start, end);
    }
}
