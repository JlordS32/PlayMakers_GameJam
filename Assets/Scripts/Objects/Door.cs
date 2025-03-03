using UnityEngine;

public class Door : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Animator animator;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering");      
    }

    public void toggleTrigger()
    {
        boxCollider.isTrigger = !boxCollider.isTrigger;
        animator.SetBool("isOpen", boxCollider.isTrigger);
    }
}
