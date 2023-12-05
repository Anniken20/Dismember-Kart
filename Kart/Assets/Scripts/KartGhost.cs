using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KartGhost : MonoBehaviour
{
    // Store player movement data for each lap
    private List<Vector3[]> lapData = new List<Vector3[]>();
    private int currentLap = 0;
    private int currentPoint = 0;

    // Function to record the player's movement for each lap
    public void RecordMovement(Vector3[] positions)
    {
        lapData.Add(positions);
    }

    // Function to play back the recorded movement
    public void PlayGhost()
    {
        StartCoroutine(PlayLap());
    }

    // Coroutine to play back the recorded movements lap by lap
    private IEnumerator PlayLap()
    {
        while (currentLap < lapData.Count)
        {
            Vector3[] currentLapData = lapData[currentLap];

            while (currentPoint < currentLapData.Length)
            {
                // Move the ghost object to the recorded position
                transform.position = currentLapData[currentPoint];
                currentPoint++;

                // Adjust the playback speed as needed
                yield return new WaitForSeconds(0.1f); // Adjust this for playback speed
            }

            // Move to the next lap
            currentPoint = 0;
            currentLap++;

            yield return new WaitForSeconds(1f); // Delay before starting the next lap
        }
    }
}