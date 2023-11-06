using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float speedBoostAmount = 2.0f; 
    public float duration = 5.0f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            KartMovement kartMovement = other.GetComponent<KartMovement>(); 
            if (kartMovement != null)
            {
                // Apply the speed boost to the kart
                kartMovement.ApplySpeedBoost(speedBoostAmount, duration);

                // Disable the pickup object or destroy it
                gameObject.SetActive(false); 
            }
        }
    }
}