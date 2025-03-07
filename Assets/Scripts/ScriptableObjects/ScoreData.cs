using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData")]
public class ScoreData : ScriptableObject, ISerializationCallbackReceiver
{
   [SerializeField] private List<string> keys = new();
   [SerializeField] private List<float> values = new();

   private Dictionary<string, float> score = new();

   public void SetScore(string key, float value)
   {
      score[key] = value;
   }

   public float GetScore(string key)
   {
      return score.TryGetValue(key, out float value) ? value : 0f;
   }

   public void OnBeforeSerialize()
   {
      keys.Clear();
      values.Clear();
      foreach (var kvp in score)
      {
         keys.Add(kvp.Key);
         values.Add(kvp.Value);
      }
   }

   public void OnAfterDeserialize()
   {
      score = new Dictionary<string, float>();
      for (int i = 0; i < keys.Count; i++)
      {
         score[keys[i]] = values[i];
      }
   }
}
