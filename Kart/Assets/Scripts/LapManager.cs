using System;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    public int totalLaps = 3;
    private int currentLap = 0;
    public List<string> checkpointsPassed = new List<string>();
    public int totalCheckpointsInALap = 3;

    private KartMovement kartMovement;
    private float startTime;
    private float endTime;

    public event Action<int> OnLapComplete;

    private void Awake()
    {
        kartMovement = GetComponent<KartMovement>();
    }

    private void Update()
    {
        if (kartMovement != null && kartMovement.HasRaceStarted() && currentLap < totalLaps)
        {
            endTime = Time.time;

            if (checkpointsPassed.Count == totalCheckpointsInALap)
            {
                CompleteLap();
            }
        }
    }

    public void OnCheckpointReached(string checkpointTag)
    {
        if (kartMovement != null && kartMovement.HasRaceStarted())
        {
            if (!checkpointsPassed.Contains(checkpointTag))
            {
                checkpointsPassed.Add(checkpointTag);
            }
        }
    }

    private void CompleteLap()
    {
        currentLap++;
        checkpointsPassed.Clear();

        if (currentLap <= totalLaps)
        {
            Debug.Log("Lap " + currentLap + " completed!");
            OnLapComplete?.Invoke(currentLap);

            if (currentLap == totalLaps)
            {
                kartMovement.FinishRace();
                Debug.Log("Race Finished!");
                Debug.Log("Total race time: " + (endTime - startTime) + " seconds");
            }
        }
    }
}