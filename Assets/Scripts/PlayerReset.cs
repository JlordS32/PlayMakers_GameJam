using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerReset : MonoBehaviour
{
    [Header("Testing")]
    private readonly KeyCode resetKey = KeyCode.R;
    private readonly KeyCode checkpointKey = KeyCode.T;

    private Vector3 checkpointPosition;

    void Awake()
    {
        checkpointPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(checkpointKey))
        {
            UpdateCheckPoint();
        }

        if (Input.GetKeyDown(resetKey)) {
            ResetPosition();
        }
    }

    void UpdateCheckPoint() {
        checkpointPosition = transform.position;
    }

    void ResetPosition()
    {
        transform.position = checkpointPosition;  
    }
}
