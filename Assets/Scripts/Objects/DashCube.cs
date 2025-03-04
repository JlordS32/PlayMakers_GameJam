using UnityEngine;

public class DashCube : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.AddDash();
            Destroy(gameObject);
        }
    }
}
