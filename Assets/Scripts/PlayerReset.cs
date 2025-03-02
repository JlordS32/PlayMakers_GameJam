using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject checkpointPrefab;
    [SerializeField] private Transform parentFolder;

    [Header("Testing")]
    private readonly KeyCode resetKey = KeyCode.R;
    private readonly KeyCode checkpointKey = KeyCode.T;

    private GameObject currentCheckpoint;

    void Update()
    {
        if (Input.GetKeyDown(checkpointKey))
        {
            UpdateCheckPoint();
        }

        if (Input.GetKeyDown(resetKey))
        {
            ResetPosition();
        }
    }

    void UpdateCheckPoint()
    {
        // Check if the parent folder is set, please set it first.
        if (parentFolder == null)
        {
            Debug.LogWarning("Parent folder is null, please set one to organise checkpoints");
            return;
        }

        // If a checkpoint already exists, destroy the previous one
        if (currentCheckpoint != null)
        {
            Destroy(currentCheckpoint);
        }

        // Instantiate new checkpoint and store the reference to currentCheckpoint (We'll destroy it later)
        currentCheckpoint = Instantiate(checkpointPrefab, transform.position, Quaternion.identity, parentFolder);
    }

    void ResetPosition()
    {
        if (currentCheckpoint == null) return;

        if (currentCheckpoint != null)
        {
            Destroy(currentCheckpoint);
            transform.position = currentCheckpoint.transform.position;
        }
    }
}
