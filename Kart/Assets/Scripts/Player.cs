using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private LapManager lapManager;

    void Start()
    {
        lapManager = FindObjectOfType<LapManager>();

        if (lapManager != null)
        {
            lapManager.OnLapComplete += HandleLapCompletion;
        }
    }

    // Function to handle lap completion event
    private void HandleLapCompletion(int lap)
    {
        Debug.Log("Player completed lap: " + lap);
    // Can put logic here to handle specific actions upon each lap completion
    }
}