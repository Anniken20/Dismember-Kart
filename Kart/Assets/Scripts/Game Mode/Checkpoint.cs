using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("References")]
    private GameObject progressionManagerGameObject;
    private ProgressionManager progressionManager;

    [Header("Values")]
    [SerializeField] private int checkpointNumber = 0;
    [SerializeField] private float timeGained = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        progressionManagerGameObject = GameObject.FindGameObjectWithTag("Progression Manager");

        if (progressionManagerGameObject)
        {
            if (progressionManagerGameObject.GetComponent<ProgressionManager>())
            {
                progressionManager = progressionManagerGameObject.GetComponent<ProgressionManager>();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Passed checkpoint: " + checkpointNumber);
            int playerID = collider.GetComponent<PlayerID>().GetPlayerID();
            progressionManager.AddToCheckpointList(checkpointNumber, timeGained, playerID);
        }
    }
}
