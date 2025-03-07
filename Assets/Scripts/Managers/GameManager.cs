using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameManager : MonoBehaviour
{
   [SerializeField] private InputAction pauseButton;
   [SerializeField] private GameObject pausePanel;

   public bool levelFinished { get; set; } = false;

   private void OnEnable()
   {
      pauseButton.Enable();
   }

   private void OnDisable()
   {
      ResetTimeScale();
   }

   private void OnDestroy()
   {
      ResetTimeScale();
   }

   void Update()
   {
      if (pausePanel == null) return;

      if (pauseButton.WasPressedThisFrame())
      {
         TogglePauseGame();
      }

      if (!pausePanel.activeSelf)
         ToggleCursor.DisableCursor();
      else
         ToggleCursor.EnableCursor();
   }

   public void TogglePauseGame()
   {
      Time.timeScale = Time.timeScale == 0 ? 1 : 0;
      pausePanel.SetActive(!pausePanel.activeSelf);
   }

   public void QuitGame()
   {
      Application.Quit();
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#endif
   }

   private void ResetTimeScale()
   {
      Time.timeScale = 1;
   }

}
