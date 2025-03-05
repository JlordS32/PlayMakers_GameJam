using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private UIManager uiManager;

    void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
    }

    void Update()
    {
        float time = Time.time;
        int minutes = Mathf.FloorToInt(time / 60);
        float seconds = time % 60;

        string timeString = minutes > 0
            ? $"{minutes}m {seconds:F2}s"
            : $"{seconds:F2}s";

        uiManager.UpdateTimerText(timeString);
    }
}
