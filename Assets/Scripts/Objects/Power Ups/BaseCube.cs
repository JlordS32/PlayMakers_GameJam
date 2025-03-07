using UnityEngine;

public class BaseCube : MonoBehaviour
{
   [SerializeField] private AudioClip collectedSound;

   protected virtual void OnCubeTriggered(Collider other) { }

   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         OnCubeTriggered(other);
         AudioManager.instance.PlaySound(collectedSound);
         Destroy(gameObject);
      }
   }
}
