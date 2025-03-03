using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float jumpForce;
    [SerializeField] private float extraGravity;

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void OnMove(InputValue value) // Callback function from Input System
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        if (value.isPressed)
        {
            speed = runningSpeed;
        }
        else
        {
            speed = initialSpeed;
        }
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
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        Debug.Log(isGrounded);

        // Lock movement to XZ plane
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Compute movement direction
        Vector3 moveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

        // Apply movement using Rigidbody

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += extraGravity * Time.deltaTime * Vector3.down;
        }
        else
        {
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
    }
}
