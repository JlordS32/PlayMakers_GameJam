using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent;
    [SerializeField] private bool triggerOnExit;
    [SerializeField] private List<string> targetTags;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (targetTags == null || targetTags.Count == 0)
        {
            Debug.LogWarning("TriggerButton: No target tags set!");
            return;
        }

        foreach (string tag in targetTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                if (!animator.GetBool("Pressed"))
                {
                    animator.SetBool("Pressed", true);
                    triggerEvent.Invoke();
                }
                return;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        animator.SetBool("Pressed", false);

        if (!triggerOnExit) return;
        triggerEvent.Invoke();
    }
}
