/*using UnityEngine;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public List<Transform> checkpoints; // List of checkpoint transforms
    public int totalLaps = 3; // Set the total number of laps
    public KartMovement kartMovement; // Reference to the KartMovement script

    private int currentCheckpointIndex = 0;
    private int currentLap = 1;

    void Start()
    {
        if (checkpoints.Count == 0)
        {
            Debug.LogError("No checkpoints assigned..");
        }
    }

    public int GetCurrentCheckpointIndex()
    {
        return currentCheckpointIndex;
    }

    public void SetCurrentCheckpointIndex(int index)
    {
        currentCheckpointIndex = index;
    }

    public int GetTotalCheckpoints()
    {
        return checkpoints.Count;
    }

    public void UpdateLap()
    {
        if (currentCheckpointIndex == 0)
        {
            currentLap++;
            if (currentLap > totalLaps)
            {
                kartMovement.EndRace(true);
            }
        }
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }

    public Transform GetCurrentCheckpoint()
    {
        return checkpoints[currentCheckpointIndex];
    }
}
*/