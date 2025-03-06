using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameManager : MonoBehaviour
{
   [SerializeField] private InputAction pauseButton;
   [SerializeField] private GameObject pausePanel;

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
         ToggleCursor.EnableCursor();
      }
   }

   public void TogglePauseGame()
   {
      Time.timeScale = Time.timeScale == 0 ? 1 : 0;
      pausePanel.SetActive(!pausePanel.activeSelf);
   }

   public void QuitGame()
   {
      Application.Quit();
      EditorApplication.isPlaying = false;
   }

   private void ResetTimeScale()
   {
      Time.timeScale = 1;
   }

}
