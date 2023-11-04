using UnityEngine;
using UnityEngine.UI;

public class LapUI : MonoBehaviour
{
    public KartMovement kartMovement;
    public Text lapText;

    void Update()
    {
        int currentLap = kartMovement.GetCurrentLap();
        lapText.text = "Lap: " + currentLap;
    }
}