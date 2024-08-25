using UnityEngine;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    private Checkpoint lastCheckpoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        lastCheckpoint = checkpoint;
        Debug.Log("Checkpoint zaliczony: " + checkpoint.name);
    }

    public Vector3 GetLastCheckpointPosition()
    {
        if (lastCheckpoint != null)
        {
            return lastCheckpoint.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
