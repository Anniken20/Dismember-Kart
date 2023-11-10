using System.Collections;
using System.Collections.Generic;
using KartGame.KartSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [Header("References")]
    private ArcadeKart arcadeKart;

    [Header("Laps")]
    [SerializeField] private int checkpointsInScene;
    private List<int> checkpointsPassed = new List<int>();
    private int currentLap = 0;
    [SerializeField] private int maxLaps;

    [Header("Time Keeping")]
    private float countdownCurrentTime;
    private float countdownStartTime = 3f;
    public bool raceStarted;
    private float currentTime;
    [SerializeField] private float startTime = 30f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI lapStatusText;
    [SerializeField] private TextMeshProUGUI timeStatusText;
    [SerializeField] private TextMeshProUGUI countdownStatusText;



    void Awake()
    {
        arcadeKart = GameObject.FindGameObjectWithTag("Player").GetComponent<ArcadeKart>();
        
        UpdateTimer(startTime);
        UpdateLap();
        Countdown();
        currentTime = startTime;
        countdownCurrentTime = countdownStartTime;
    }
    void Update()
    {
        if (raceStarted)
        {
            currentTime -= Time.deltaTime;
            UpdateTimer(currentTime);

            if (currentLap >= maxLaps)
            {
                Win();
            }
            
            if (currentTime <= 0f)
            {
                Lose();
            }
        }
        else
        {
            if (countdownCurrentTime >= 0)
            {
                arcadeKart.SetCanMove(false);
                Countdown();
                countdownCurrentTime -= Time.deltaTime;
            }
            else
            {
                countdownStatusText.enabled = false;
                arcadeKart.SetCanMove(true);
                raceStarted = true;
            }
        }
    }

    // -------------------------------------
    // LAP STUFF
    public void AddToCheckpointList(int newCheckpoint, float timeGained)
    {
        if (!checkpointsPassed.Contains(newCheckpoint))
        {
            AddTime(timeGained);
            checkpointsPassed.Add(newCheckpoint);
        }
    }

    public void UpdateLap()
    {
        if (checkpointsPassed.Count >= checkpointsInScene)
        {
            Debug.Log("Lap Completed");
            RefreshCheckpoints();
            currentLap++;
        }

        lapStatusText.text = string.Format(currentLap + " / " + maxLaps);
    }

    private void RefreshCheckpoints()
    {
        checkpointsPassed.Clear();
    }

    // -------------------------------------
    // TIME STUFF
    private void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeStatusText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Debug.Log(minutes + ":" + seconds);
    }

    public void AddTime(float timeGained)
    {
        currentTime += timeGained;
        UpdateTimer(currentTime);
    }

    private void Countdown()
    {
        int cutoff = (int)countdownCurrentTime;
        countdownStatusText.text = cutoff.ToString();
    }


    // -------------------------------------
    // WIN/LOSE

    private void Win()
    {
        Debug.Log("Ya win!");
    }

    private void Lose()
    {
        Debug.Log("Ya lose!");
    }

    // -------------------------------------
    

}
