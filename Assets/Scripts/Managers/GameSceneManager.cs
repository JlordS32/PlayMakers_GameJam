using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static int level;

    void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }

    public static void LoadNextScene()
    {
        level++;
        SceneManager.LoadScene(level);
    }

    public static void LoadScene(int value)
    {

        SceneManager.LoadScene(value, LoadSceneMode.Single);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
