using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Game/ObjectData")]
public class SpecialObject : ScriptableObject
{
   public Dictionary<string, Vector3> positions = new();
   public Dictionary<string, bool> hasSavedPosition = new();

   public void AddPosition(string name, Vector3 position)
   {
      positions.Add(name, position);
      hasSavedPosition.Add(name, true);
   }

   public Vector3 GetPosition(string name)
   {
      return positions[name];
   }

   public bool HasSavedPosition(string name)
   {
      if (hasSavedPosition.ContainsKey(name)) {
         return hasSavedPosition[name];
      }

      return false;
   }

   public void UpdatePosition(string name, Vector3 position)
   {
      positions[name] = position;
   }

   public void ResetData() {
      positions.Clear();
      hasSavedPosition.Clear();
   }
}