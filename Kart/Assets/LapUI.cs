using UnityEngine;
using UnityEngine.UI;

public class LapUI : MonoBehaviour
{
    public LapManager lapManager;
    public Text lapText;

    private void Start()
    {
        if (lapManager == null)
        {
            Debug.LogError("LapManager not assigned to LapUI.");
            enabled = false;
        }
        lapText.text = "Lap: 0 / " + lapManager.totalLaps;
        lapManager.OnLapComplete += UpdateLapUI;
    }

    private void UpdateLapUI(int lap)
    {
        lapText.text = "Lap: " + lap + " / " + lapManager.totalLaps;
    }
}