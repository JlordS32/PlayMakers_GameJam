using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerReset : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject checkpointPrefab;
    [SerializeField] private Transform parentFolder;
    [SerializeField] private PlayerData playerData;

    private GameObject currentCheckpoint;
    private PlayerMovement playerMovement;

    void Awake()
    {
        if (playerData.hasSavedPosition)
        {
            transform.position = playerData.playerPosition;
        }
        else
        {
            playerData.playerPosition = transform.position; // Store the default position
            playerData.hasSavedPosition = true;
        }

        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnCheckpoint(InputValue value) {
        if (value.isPressed) UpdateCheckPoint();
    }

    public void OnRestart(InputValue value) {
        if (value.isPressed) ResetPosition();
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
            playerData.playerPosition = currentCheckpoint.transform.position;
            playerData.dashes = playerMovement.dashes;
            playerData.extraJumps = playerMovement.extraJumps;
            Destroy(currentCheckpoint);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnApplicationQuit()
    {
        playerData.ResetData();
    }

#if UNITY_EDITOR
    void OnDisable()
    {
        if (!Application.isPlaying)
        {
            playerData.ResetData();
        }
    }
#endif
}
