using NUnit.Framework.Internal;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Parameters")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float runningSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;

    [Header("Dash Parameters")]
    [SerializeField] private float dashingSpeed = 1f;
    [SerializeField] private float delayTime = 1;

    [Header("Falling Parameters")]
    [SerializeField] private float extraGravity = 1f;
    [SerializeField] private float groundCheckDistance = 0.2f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundLayer;

    // Public variables
    private int dashes = 0;
    private int extraJumps = 0;

    // Private variables
    private Vector2 movementInput;
    private Rigidbody rb;
    private float initialSpeed;
    private bool isGrounded;

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
        StopCoroutine(Accelerate(value.isPressed));
        StartCoroutine(Accelerate(value.isPressed));
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
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
        else
        {
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
    }
    #endregion

    #region PUBLIC_METHODS
    public void AddDash()
    {
        dashes++;
        Debug.Log("Dash obtained!");
    }

    public void AddJump()
    {
        extraJumps++;
        Debug.Log("Jump obtained!");
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

    IEnumerator Accelerate(bool isSprinting = false)
    {
        if (isSprinting)
        {
            if (isGrounded)
            {
                speed = runningSpeed;
            }
            else if (dashes > 0 && !isGrounded)
            {
                Debug.Log("Dashing");
                speed = dashingSpeed;
                dashes--;
            }
            else
            {
                speed = initialSpeed;
            }
        }
        else
        {
            speed = initialSpeed;
        }

        yield return new WaitForSeconds(delayTime);

        speed = isGrounded ? runningSpeed : initialSpeed;
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
