/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthPickup : MonoBehaviour
{
    public float growthFactor = 1.5f; // Factor by which the player grows when collecting the pickup.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            // Apply growth to the player character.
            KartMovement playerController = other.GetComponent<KartMovement>(); 
            if (playerController != null)
            {
                playerController.Grow(growthFactor);
            }

            gameObject.SetActive(false); 
        }
    }
}
*/