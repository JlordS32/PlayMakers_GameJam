using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent;
    [SerializeField] private bool triggerOnExit;
    [SerializeField] private List<string> targetTags;

    private Animator animator;
    private AudioSource audioSource;
    private HashSet<GameObject> objectsOnButton = new HashSet<GameObject>();

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hey");
        audioSource.Play();
        if (targetTags == null || targetTags.Count == 0)
        {
            Debug.LogWarning("TriggerButton: No target tags set!");
            return;
        }

        foreach (string tag in targetTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                objectsOnButton.Add(other.gameObject);

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
        audioSource.Play();
        foreach (string tag in targetTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                objectsOnButton.Remove(other.gameObject); 

                if (objectsOnButton.Count == 0)
                {
                    animator.SetBool("Pressed", false);
                    if (triggerOnExit)
                    {
                        triggerEvent.Invoke();
                    }
                }
                return;
            }
        }
    }
}
