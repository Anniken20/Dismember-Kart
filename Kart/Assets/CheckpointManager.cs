using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string checkpointTag; // Unique tag for this checkpoint

    private LapManager lapManager;

    private void Start()
    {
        lapManager = FindObjectOfType<LapManager>();

        if (lapManager == null)
        {
            Debug.LogError("LapManager not found! Ensure LapManager is in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lapManager.OnCheckpointReached(checkpointTag);
        }
    }
}