using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private Transform cameraTransform;

    private Vector2 movementInput;
    private Rigidbody rb;
    private float initialSpeed;

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

    public void OnSprint(InputValue value) {
        if (value.isPressed) {
            speed = runningSpeed;
        } else {
            speed = initialSpeed;
        }
    }

    void Update()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Lock movement to XZ plane
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Compute movement direction
        Vector3 moveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

        // Apply movement using Rigidbody
        rb.linearVelocity = speed * moveDirection;
    }
}
