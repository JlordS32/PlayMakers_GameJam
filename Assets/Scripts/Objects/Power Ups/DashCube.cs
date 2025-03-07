using UnityEngine;

public class DashCube : BaseCube
{
    protected override void OnCubeTriggered(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.AddDash();
    }
}
