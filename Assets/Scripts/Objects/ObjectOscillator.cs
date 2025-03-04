using UnityEngine;

public class ObjectOscillator : MonoBehaviour
{
    [SerializeField] float oscillationSpeed = 1f;
    [SerializeField] float oscillationDistance = 1f;

    private Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float oscillation = Mathf.PingPong(Time.time * oscillationSpeed, oscillationDistance * 2) - oscillationDistance;
        transform.position = initialPosition + new Vector3(oscillation, 0, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = initialPosition - new Vector3(oscillationDistance, 0, 0);
        Vector3 end = initialPosition + new Vector3(oscillationDistance, 0, 0);
        Gizmos.DrawLine(start, end);
    }

}
