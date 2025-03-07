using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("Base Parameters")]
    [SerializeField] private PlayerData playerData;
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

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpingSound;
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip runningSound;
    [SerializeField] private AudioClip dashingSound;

    // Public variables
    public int dashes { get; private set; } = 0;
    public int extraJumps { get; private set; } = 0;

    // Private variables
    private Vector2 movementInput;
    private Rigidbody rb;
    private float initialSpeed;
    private bool isGrounded;
    private bool isWalkingSoundPlaying = false;
    private bool isRunningSoundPlaying = false;
    private bool isJumpingSoundPlaying = false;
    private bool isDashingSoundPlaying = false;

    #endregion
    #region UNITY_FUNCTIONS
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        initialSpeed = speed;

        if (playerData.hasSavedPosition)
        {
            dashes = playerData.dashes;
            extraJumps = playerData.extraJumps;
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
            AudioManager.instance.PlaySound(runningSound);
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
        if (isGrounded)
        {
            if (!isJumpingSoundPlaying)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                AudioManager.instance.PlaySound(jumpingSound);
                isJumpingSoundPlaying = true;
            }
        }
        else
        {
            if (extraJumps > 0 && !isDashingSoundPlaying)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                AudioManager.instance.PlaySound(dashingSound);
                extraJumps--;
                isDashingSoundPlaying = true;
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
            // Play walking sound when moving
            if (movementInput.magnitude > 0 && !isWalkingSoundPlaying)
            {
                AudioManager.instance.PlaySound(walkingSound);
                isWalkingSoundPlaying = true;
            }
            else if (movementInput.magnitude == 0)
            {
                isWalkingSoundPlaying = false;
            }

            // Play running sound when sprinting
            if (movementInput.magnitude > 0 && !isRunningSoundPlaying && speed == runningSpeed)
            {
                AudioManager.instance.PlaySound(runningSound);
                isRunningSoundPlaying = true;
            }
            else if (movementInput.magnitude == 0 || speed != runningSpeed)
            {
                isRunningSoundPlaying = false;
            }

            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
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

        // Reset jump/dash sounds when the player lands
        if (isGrounded)
        {
            ResetJumpAndDashSounds();
        }
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

    void ResetJumpAndDashSounds()
    {
        isJumpingSoundPlaying = false;
        isDashingSoundPlaying = false;
    }

    #endregion
}
