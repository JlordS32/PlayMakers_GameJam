using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Object"))
        {
            animator.SetBool("Pressed", true);
            triggerEvent.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        animator.SetBool("Pressed", false);
        triggerEvent.Invoke();
    }
}
