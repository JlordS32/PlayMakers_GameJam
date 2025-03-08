using UnityEngine;

public class CreditScene : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
