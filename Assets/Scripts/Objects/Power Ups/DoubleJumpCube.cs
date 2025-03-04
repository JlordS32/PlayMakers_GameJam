using UnityEngine;

public class DoubleJumpCube : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.AddJump();
            Destroy(gameObject);
        }
    }
}
