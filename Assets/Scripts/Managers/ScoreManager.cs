using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ScoreData scoreData;

    private UIManager uiManager;
    private GameManager gameManager;
    private float timeElapsed = 0f;
    private string sceneName;

    void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        sceneName = SceneManager.GetActiveScene().name;

        if (playerData.hasSavedPosition)
        {
            if (playerData.timeElapsed.ContainsKey(sceneName))
            {
                timeElapsed = playerData.timeElapsed[sceneName];
            }
            else
            {
                playerData.timeElapsed[sceneName] = timeElapsed;
            }
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        float seconds = timeElapsed % 60;

        string timeString = minutes > 0
            ? $"{minutes}m {seconds:F2}s"
            : $"{seconds:F2}s";

        uiManager.UpdateTimerText(timeString);

        if (gameManager.levelFinished)
        {
            scoreData.SetScore(sceneName, timeElapsed);
            gameManager.levelFinished = false;
        }
    }

    void OnDisable()
    {
        playerData.timeElapsed[sceneName] = timeElapsed;
    }

}
