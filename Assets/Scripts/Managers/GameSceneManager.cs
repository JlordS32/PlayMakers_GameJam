using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private int level;

    void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        level++;
        playerData.ResetData();
        SceneManager.LoadScene(level);
    }

    public void LoadScene(string sceneName)
    {
        playerData.ResetData();
        SceneManager.LoadScene(sceneName);
    }
}
