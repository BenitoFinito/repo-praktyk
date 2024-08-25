using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void Awake()
    {
       
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>(); 
        }
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetLastCheckpoint(this);
        }
    }
}
