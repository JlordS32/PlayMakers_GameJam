using UnityEngine;

public class ToggleCursor
{
   public static void Toggle()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }
}