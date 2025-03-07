using UnityEngine;

public class ObjectOscillator : MonoBehaviour
{
    [SerializeField] private float oscillationSpeed = 1f;
    [SerializeField] private float oscillationDistance = 1f;
    [SerializeField] private bool centre = true;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool force = true;
    [Range(-1, 1)]
    [SerializeField] private float oscillationX;
    [Range(-1, 1)]
    [SerializeField] private float oscillationY;
    [Range(-1, 1)]
    [SerializeField] private float oscillationZ;
    [SerializeField] private int cycles = 1;

    private Vector3 initialPosition;
    private float cycle = 0f;
    private bool trigger = false;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!force && !trigger) return;
        Oscillate();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (centre)
        {
            Vector3 start = initialPosition - new Vector3(oscillationX * oscillationDistance, oscillationY * oscillationDistance, oscillationZ * oscillationDistance);
            Vector3 end = initialPosition + new Vector3(oscillationX * oscillationDistance, oscillationY * oscillationDistance, oscillationZ * oscillationDistance);
            Gizmos.DrawLine(start, end);
        }
        else
        {
            Vector3 end = initialPosition + new Vector3(oscillationX * oscillationDistance * 2, oscillationY * oscillationDistance * 2, oscillationZ * oscillationDistance * 2);
            Gizmos.DrawLine(initialPosition, end);
        }
    }

    private void Oscillate()
    {
        cycle += Time.deltaTime * oscillationSpeed;

        float oscillation = Mathf.PingPong(Time.time * oscillationSpeed, oscillationDistance * 2) + (centre ? -oscillationDistance : 0);
        transform.position = initialPosition + new Vector3(oscillationX * oscillation, oscillationY * oscillation, oscillation * oscillationZ);

        if (!loop && cycle >= oscillationDistance * 2) // One cycle
        {
            if (cycles > 0)
            {
                cycles--;
                cycle = 0f;
            }
            else
            {
                force = trigger = false;
            }
        }
    }

    public void TriggerOscillate()
    {
        trigger = true;
    }

    // Detect collision with the platform
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        foreach (ContactPoint contact in collision.contacts)
        {
           Vector3 contactNormal = contact.normal;
            if (Vector3.Dot(contactNormal, Vector3.down) > 0.5f) // If collision came from the top
            {
                obj.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        obj.transform.SetParent(null);
    }
}
