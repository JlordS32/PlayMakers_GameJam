using UnityEngine;

public class CubeSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 50f;
    [SerializeField] private float oscillationSpeed = 1f; // Speed of the oscillation
    [SerializeField] private float oscillationHeight = 0.5f; // Height of the oscillation

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Rotate the cube around its local axes (X, Y)
        transform.Rotate(Vector3.one * spinSpeed * Time.deltaTime, Space.Self);

        // Oscillate between two points.
        float oscillation = Mathf.Sin(Time.time * oscillationSpeed) * oscillationHeight;
        transform.position = initialPosition + new Vector3(0, oscillation, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 oscillationTarget = initialPosition + new Vector3(0, oscillationHeight, 0);
        Gizmos.DrawLine(initialPosition, oscillationTarget);
    }
}
