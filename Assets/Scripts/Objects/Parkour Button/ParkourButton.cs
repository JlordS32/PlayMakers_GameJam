using System.Collections;
using UnityEngine;

public class ParkourTrigger : MonoBehaviour
{
    public GameObject[] parkourObjects;
    public float duration = 12f;

    private void Start()
    {
        foreach (GameObject obj in parkourObjects)
        {
            obj.SetActive(false); 
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShowAndHideParkour());
        }
    }


    private IEnumerator ShowAndHideParkour()
    {
        Debug.Log("Show parkour objects");

        foreach (GameObject obj in parkourObjects)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        foreach (GameObject obj in parkourObjects)
        {
            obj.SetActive(false);
        }
    }
}
