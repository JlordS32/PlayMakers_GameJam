using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI powerUpsText;

    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        powerUpsText.text = $"Dashes: {playerMovement.dashes}\t Jumps: {playerMovement.extraJumps}";
    }

    public void UpdateTimerText(string newText)
    {
        if (timerText != null)
        {
            timerText.text = newText;
        }
    }
}