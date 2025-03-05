using UnityEngine;
using TMPro;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private float decayTime;
    [SerializeField] private string message;

    private TextMeshProUGUI triggerText;
    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        if (!hasTriggered)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(TriggerText());
                hasTriggered = true;
            }
        }
    }

    IEnumerator TriggerText()
    {
        textObject.SetActive(true);
        triggerText = textObject.GetComponent<TextMeshProUGUI>();

        if (triggerText != null)
        {
            triggerText.text = message;
        }

        yield return new WaitForSeconds(decayTime);

        triggerText.text = "";
        textObject.SetActive(false);
    }
}
