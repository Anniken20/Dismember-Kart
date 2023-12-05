using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KartGame.KartSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    [Header("References")]
    private List<GameObject> kartObjects = new List<GameObject>();
    private List<ArcadeKart> arcadeKarts = new List<ArcadeKart>();
    [Header("Two Player Mode")]
    [SerializeField] private bool twoPlayerMode;
    [SerializeField] private TextMeshProUGUI player2LapStatusText;
    [SerializeField] private TextMeshProUGUI player2TimeStatusText;
    private List<int> player2CheckpointsPassed = new List<int>();
    private int player2CurrentLap = 0;
    private float player2CurrentTime;

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
    [Header("Audio")]
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private AudioClip LapSound;

    void Awake()
    {
        kartObjects = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
        foreach(GameObject kartObject in kartObjects)
        {
            arcadeKarts.Add(kartObject.GetComponent<ArcadeKart>());
        }
        
        UpdateTimer(startTime);
        if (twoPlayerMode)
        {
            UpdateTimerForPlayer2(player2CurrentTime);
        }
        UpdateLap(-1);
        Countdown();
        currentTime = startTime;
        if (twoPlayerMode)
        {
            player2CurrentTime = startTime;
        }

        countdownCurrentTime = countdownStartTime;
    }
    void Update()
    {
        if (raceStarted)
        {
           if (!MenuManager.InfiniteTimeEnable)
            {
                currentTime -= Time.deltaTime;
                UpdateTimer(currentTime);
                if (twoPlayerMode)
                {
                    player2CurrentTime -= Time.deltaTime;
                    UpdateTimerForPlayer2(player2CurrentTime);
                }
                
            }
            

            if (currentLap >= maxLaps)
            {
                Win(1); // for player 1
            }
            if (player2CurrentLap >= maxLaps)
            {
                Win(2); // for player 2
            }
            
            if (currentTime <= 0f)
            {
                Lose(1); // for player 1
            }
            if (player2CurrentTime <= 0 && twoPlayerMode)
            {
                Lose(2); // for player 2
            }
        }
        else
        {
            if (countdownCurrentTime >= 0)
            {
                foreach(ArcadeKart arcadeKart in arcadeKarts)
                {
                    arcadeKart.SetCanMove(false);
                }
                Countdown();
                countdownCurrentTime -= Time.deltaTime;
            }
            else
            {
                countdownStatusText.enabled = false;
                foreach(ArcadeKart arcadeKart in arcadeKarts)
                {
                    arcadeKart.SetCanMove(true);
                }
                raceStarted = true;
            }
        }
    }

    // -------------------------------------
    // LAP STUFF
    public void AddToCheckpointList(int newCheckpoint, float timeGained, int playerID)
    {
        if (!checkpointsPassed.Contains(newCheckpoint))
        {
            if (playerID == 1)
            {
                AddTime(timeGained, 1);
                checkpointsPassed.Add(newCheckpoint);
            }
            else
            {
                AddTime(timeGained, 2); // but for player two!
                player2CheckpointsPassed.Add(newCheckpoint); // but for player two
            }

        }
    }

    public void UpdateLap(int playerID)
    {
        if (checkpointsPassed.Count >= checkpointsInScene)
        {
            Debug.Log("Lap Completed");
            RefreshCheckpoints(1);
            currentLap++;
        }
        if (player2CheckpointsPassed.Count >= checkpointsInScene)
        {
            RefreshCheckpoints(2);
            currentLap++;
        }

        lapStatusText.text = string.Format(currentLap + " / " + maxLaps);
        if (twoPlayerMode)
        {
            player2LapStatusText.text = string.Format(player2CurrentLap + " / " + maxLaps);
        }
    }

    private void RefreshCheckpoints(int PlayerID)
    {
        if (PlayerID == 1)
        {
            checkpointsPassed.Clear();
        }
        else
        {
            player2CheckpointsPassed.Clear();
        }
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

    private void UpdateTimerForPlayer2(float player2CurrentTime)
    {
        float minutes = Mathf.FloorToInt(player2CurrentTime / 60);
        float seconds = Mathf.FloorToInt(player2CurrentTime % 60);

        player2TimeStatusText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddTime(float timeGained, int playerID)
    {
        if (playerID == 1)
        {
            currentTime += timeGained;
            UpdateTimer(currentTime);
        }
        else
        {
            player2CurrentTime += timeGained;
            UpdateTimerForPlayer2(player2CurrentTime);
        }

    }

    private void Countdown()
    {
        int cutoff = (int)countdownCurrentTime;
        countdownStatusText.text = cutoff.ToString();
    }

    // -------------------------------------
    // WIN/LOSE

    private void Win(int playerID)
    {
        if (playerID == 1)
        {
            SceneManager.LoadScene("P1WinScene");
        }
        else
        {
            SceneManager.LoadScene("P2WinScene");
        }
    }

    private void Lose(int playerID)
    {
        if (twoPlayerMode)
        {
            if (playerID == 1)
            {
                SceneManager.LoadScene("P2WinScene");
            }
            if (playerID == 2)
            {
                SceneManager.LoadScene("P1WinScene");
            }
        }
        SceneManager.LoadScene("LoseScene");
    }

    // -------------------------------------
}
