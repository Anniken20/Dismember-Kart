using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Vector3 growScale = new Vector3(2f, 2f, 2f); // Scale to grow when power-up is collected
    public Vector3 shrinkScale = new Vector3(0.5f, 0.5f, 0.5f); // Scale to shrink when power-up is collected
    public float effectDuration = 5.0f; // Duration of the power-up effect

    private bool collected = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale; // Store the original scale of the object
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            collected = true;

            ApplyPowerUpEffect();
            // Disable the power-up object or play a collect animation
            gameObject.SetActive(false); // For simplicity, deactivate the power-up after being collected

            // Schedule reverting to the original size after effectDuration seconds
            Invoke("RevertSize", effectDuration);
        }
    }

    void ApplyPowerUpEffect()
    {
        // Increase size
        transform.localScale = growScale; // You may want to adjust the scale factors here

        // Alternatively, to shrink the object, use the following line instead:
        // transform.localScale = shrinkScale;
    }

    void RevertSize()
    {
        // Revert to the original size
        transform.localScale = originalScale;

        // Reset collected status to allow collecting the power-up again
        collected = false;
    }
}