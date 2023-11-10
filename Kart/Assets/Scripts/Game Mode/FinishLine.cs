using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header("References")]
    private GameObject progressionManagerGameObject;
    private ProgressionManager progressionManager;
    
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
        Debug.Log("Something entered!");
        if (collider.CompareTag("Player"))
        {
            Debug.Log("It was a player!");
            progressionManager.UpdateLap();
        }
    }
}
