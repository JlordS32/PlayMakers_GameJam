using System.Collections;
using UnityEngine;

public class OutOfBoundCheck : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;

    public Transform player;
    public float x, y, z;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ResetPosition());
        }
    }

    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(delay);
        player.position = new Vector3(x, y, z);
    }
}
