using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class TimeTrialManager : MonoBehaviour
{
    public Text timerText;
    public Text countdownText; // Add a UI Text for the countdown
    public Transform playerKart;
    public float targetTime = 120.0f; // Set the target time for the race
    public Transform finishLine; // Reference to the finish line trigger
    private float startTime;
    private bool isRaceActive = false;
    private bool isRaceFinished = false;

    private int countdownValue = 3; // Set the initial countdown value
    private Coroutine countdownCoroutine; // Reference to the countdown coroutine

    void Start()
    {
        // Initialize the countdown
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.text = countdownValue.ToString();
        yield return new WaitForSeconds(1);

        while (countdownValue > 0)
        {
            countdownValue--;
            countdownText.text = countdownValue.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);

        countdownText.gameObject.SetActive(false); // Hide the countdown text
        StartRace();
    }

    void Update()
    {
        if (isRaceActive)
        {
            UpdateTimer();
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

    public void AddTime(float timeToAdd)
    {
        targetTime += timeToAdd; // Add the specified time to the target time
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform == finishLine)
        {
            if (!isRaceFinished)
            {
                // Load the win scene
                SceneManager.LoadScene("WinScene");
                Debug.Log("Player crossed the finish line");
                EndRace(true); // Player crossed the finish line - triggering a winning condition
            }
        }
    }
}