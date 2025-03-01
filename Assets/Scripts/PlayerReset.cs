using UnityEngine;
using StarterAssets;

public class PlayerReset : MonoBehaviour
{
    private Vector3 resetPosition;
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        resetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Initial Pos: " + resetPosition);
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        transform.position = resetPosition;  
    }
}
