using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int delay = 2;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;

    private BoxCollider boxCollider;
    private AudioSource audioSource;
    private Animator animator;
    private GameSceneManager gameSceneManager;
    private GameManager gameManager;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        gameSceneManager = FindFirstObjectByType<GameSceneManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GoNextLevel());
    }

    public void toggleTrigger()
    {
        if (!boxCollider.isTrigger)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }
        else
        {
            audioSource.PlayOneShot(doorCloseSound);
        }

        boxCollider.isTrigger = !boxCollider.isTrigger;
        animator.SetBool("isOpen", boxCollider.isTrigger);

    }

    IEnumerator GoNextLevel()
    {
        gameManager.levelFinished = true;
        yield return new WaitForSeconds(delay);
        gameManager.levelFinished = false;
        gameSceneManager.LoadNextScene();
    }
}
