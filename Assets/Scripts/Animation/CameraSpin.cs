using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 1f;

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}

