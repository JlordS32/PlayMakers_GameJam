using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float y;
    [SerializeField] private float spawnTime;   


    public GameObject prefab;
    public Transform spawnerBase;
    private GameObject currentObject;

    void checkAndSpawn()
    {
        if (currentObject == null)
        {
            spawnObject();
        }
    }

    void spawnObject()
    {
        GameObject player = GameObject.Find("Player");

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        // Check if the player has extra jumps
        if (playerMovement.getExtraJumps() == 0)
        {
            // Get the platform's position
            Vector3 spawnPosition = spawnerBase.position + new Vector3(0, y, 0);

            // Spawn the object on the platform
            currentObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }


    void Start()
    {

        spawnObject();
        InvokeRepeating(nameof(checkAndSpawn), spawnTime, spawnTime);

    }
}
