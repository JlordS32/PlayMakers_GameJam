using UnityEngine;

public class SpecialObjectReset : MonoBehaviour
{
   [SerializeField] private SpecialObject objectData;
   [SerializeField] private string key;

   private void Awake()
   {
      key ??= this.gameObject.name;
      Debug.Log($"Assigned key: {key}");

      if (!objectData.HasSavedPosition(key))
      {
         Debug.Log($"Key {key} not found, adding new position.");
         objectData.AddPosition(key, transform.position);
      }
      else
      {
         Debug.Log($"Key {key} found, setting position.");
         transform.position = objectData.GetPosition(key);
      }
   }


   void Update()
   {
      if (Input.GetKeyDown(KeyCode.T))
      {
         UpdateCheckPoint();
      }
   }

   void UpdateCheckPoint()
   {
      // Instantiate new checkpoint and store the reference to currentCheckpoint (We'll destroy it later)
      objectData.UpdatePosition(key, transform.position);
   }

   void OnApplicationQuit()
   {
      objectData.ResetData();
   }

#if UNITY_EDITOR
   void OnDisable()
   {
      if (!Application.isPlaying)
      {
         objectData.ResetData();
      }
   }
#endif
}