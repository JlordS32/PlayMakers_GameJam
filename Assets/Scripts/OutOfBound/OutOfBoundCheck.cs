using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class OutOfBoundCheck : MonoBehaviour
{

    public Transform player;
    public float x, y, z;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.position = new Vector3(x, y, z); 
        }
    }
}
