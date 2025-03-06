using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("Base Parameters")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float runningSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;

    [Header("Dash Parameters")]
    [SerializeField] private float dashingSpeed = 1f;
    [SerializeField] private float decayTime = 1;

    [Header("Falling Parameters")]
    [SerializeField] private float extraGravity = 1f;
    [SerializeField] private float groundCheckDistance = 0.2f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundLayer;

    // Public variables
    public int dashes { get; private set; } = 0;
    public int extraJumps { get; private set; } = 0;

    // Private variables
    private Vector2 movementInput;
    private Rigidbody rb;
    private float initialSpeed;
    private bool isGrounded;
    #endregion
    #region UNITY_FUNCTIONS
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
        if (value.isPressed)
        {
            if (isGrounded) UpdateSpeed(value.isPressed);
            else
            {
                if (dashes > 0) StartCoroutine(Dash());
            }
        }
        else UpdateSpeed(value.isPressed);
    }

    public void OnJump()
    {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        else
        {
            if (extraJumps > 0)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                extraJumps--;
            }
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
            rb.linearVelocity += extraGravity * Time.deltaTime * Vector3.down;
        }
        else rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }
    #endregion

    #region PUBLIC_METHODS
    public void AddDash()
    {
        if (dashes == 0)
        {
            dashes++;
            Debug.Log("Dash obtained!");
        }
        else
        {
            Debug.Log("You already have a dash!");
        }
    }

    public void AddJump()
    {
        if (extraJumps == 0)
        {
            extraJumps++;
            Debug.Log("Jump obtained!");
        }
        else
        {
            Debug.Log("You already have an extra jump!");
        }
    }

    public int getExtraJumps()
    {
        return extraJumps;
    }

    #endregion

    #region PRIVATE_METHODS
    // We're using raycast to efficiently check if the player is on the ground.
    // Feel free to change the value of `groundCheckDistance`.
    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void UpdateSpeed(bool isSprinting)
    {
        if (isSprinting && isGrounded)
        {
            speed = runningSpeed;
        }
        else
        {
            speed = initialSpeed;
        }
    }

    IEnumerator Dash()
    {
        speed = dashingSpeed;
        dashes--;
        yield return new WaitForSeconds(decayTime);
        speed = initialSpeed;
    }

    // This is the red line below the player.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.down * groundCheckDistance;
        Gizmos.DrawLine(start, end);
    }
    #endregion
}
