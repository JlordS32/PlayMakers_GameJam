using UnityEngine;

public class DoubleJumpCube : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement.getExtraJumps() == 0)
            {
                playerMovement.AddJump();
                Destroy(gameObject);
            }
        }
    }
}
