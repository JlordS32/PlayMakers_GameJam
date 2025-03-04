using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{
   public Vector3 playerPosition;
   public bool hasSavedPosition = false;

   public void ResetData()
   {
      hasSavedPosition = false;
      playerPosition = Vector3.zero;
   }
}
