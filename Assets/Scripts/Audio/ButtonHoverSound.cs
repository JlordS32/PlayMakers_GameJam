using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource && hoverSound)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}
