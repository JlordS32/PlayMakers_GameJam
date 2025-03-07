using UnityEngine;

public class DoubleJumpCube : BaseCube
{
    protected override void OnCubeTriggered(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.AddJump();
    }
}
