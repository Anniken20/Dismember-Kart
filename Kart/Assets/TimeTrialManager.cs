using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//How do you lose and win insert at the bottom required

public class TimeTrialManager : MonoBehaviour
{
    public Text timerText;
    public Transform playerKart;
    public float targetTime = 120.0f; // Set the target time for the race
    private float startTime;
    private bool isRaceActive = false;
    private bool isRaceFinished = false;

    void Start()
    {
        // Can start the race using a UI button or some other trigger
        StartRace();
    }

    void Update()
    {
        if (isRaceActive)
        {
            UpdateTimer();

            // Check if the race is finished
            if (Time.time - startTime >= targetTime && !isRaceFinished)
            {
                EndRace(true);
            }
        }
    }

    void UpdateTimer()
    {
        if (isRaceFinished)
        {
            return; // Stop updating the timer when the race is finished
        }

        float currentTime = Time.time - startTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        timerText.text = timeSpan.ToString("mm':'ss'.'ff");
    }

    void StartRace()
    {
        startTime = Time.time;
        isRaceActive = true;
    }

    void EndRace(bool isWinner)
    {
        isRaceActive = false;
        isRaceFinished = true;

        if (isWinner)
        {
            // Load the win scene
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            // Load the lose scene
            SceneManager.LoadScene("LoseScene");
        }
    }
}

