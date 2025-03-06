using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int delay = 2;

    private BoxCollider boxCollider;
    private Animator animator;
    private GameSceneManager gameSceneManager;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        gameSceneManager = FindFirstObjectByType<GameSceneManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GoNextLevel());
    }

    public void toggleTrigger()
    {
        boxCollider.isTrigger = !boxCollider.isTrigger;
        animator.SetBool("isOpen", boxCollider.isTrigger);
    }

    IEnumerator GoNextLevel() {
        yield return new WaitForSeconds(delay);
        gameSceneManager.LoadNextScene();
    } 
}
