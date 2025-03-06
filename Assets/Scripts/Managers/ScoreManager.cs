using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private UIManager uiManager;
    private float timeElapsed = 0f;

    void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        string sceneName = SceneManager.GetActiveScene().name;

        if (playerData.timeElapsed.ContainsKey(sceneName))
        {
            timeElapsed = playerData.timeElapsed[sceneName];
        }
        else
        {
            playerData.timeElapsed[sceneName] = timeElapsed;
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

        foreach (KeyValuePair<string, float> entry in playerData.timeElapsed) {
            Debug.Log(entry.Key + ": " + entry.Value);
        }
    }

    void OnDisable()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        playerData.timeElapsed[sceneName] = timeElapsed;
    }

}
