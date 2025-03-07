using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{
   public Vector3 playerPosition;
   public bool hasSavedPosition = false;
   public Dictionary<string, float> timeElapsed = new();
   public int extraJumps = 0;
   public int dashes = 0;

   public void ResetData()
   {
      hasSavedPosition = false;
      playerPosition = Vector3.zero;
      timeElapsed.Clear();
      extraJumps = 0;
      dashes = 0;
   }
}
