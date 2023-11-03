using UnityEngine;
using System;
using System.Collections.Generic;

public class LapManager : MonoBehaviour
{
    public int totalLaps = 3;
    private int currentLap = 0;
    private List<string> checkpointsPassed = new List<string>();
    public int totalCheckpointsInALap = 3; // Specify the total number of checkpoints in a lap

    public event Action<int> OnLapComplete;

    public void OnCheckpointReached(string checkpointTag)
    {
        if (!checkpointsPassed.Contains(checkpointTag))
        {
            checkpointsPassed.Add(checkpointTag);
            if (checkpointsPassed.Count == totalCheckpointsInALap)
            {
                CompleteLap();
            }
        }
    }

    public void CompleteLap()
    {
        currentLap++;
        checkpointsPassed.Clear(); // Reset checkpoints for a new lap

        if (currentLap <= totalLaps)
        {
            Debug.Log("Lap " + currentLap + " completed!");
            OnLapComplete?.Invoke(currentLap);
        }
        else
        {
            Debug.Log("Race Finished!");
        }
    }
}