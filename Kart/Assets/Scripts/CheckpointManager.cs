using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Transform> Checkpoints = new List<Transform>();

    private int currentCheckpointIndex = 0;

    public Transform GetNextCheckpoint()
    {
        if (Checkpoints.Count == 0)
            return null;

        currentCheckpointIndex = (currentCheckpointIndex + 1) % Checkpoints.Count;
        return Checkpoints[currentCheckpointIndex];
    }
}