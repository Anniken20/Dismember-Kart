using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMovement : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 10f;
    public float braking = 10f;
    public float steering = 5f;
    public float checkpointTolerance = 2.0f;

    private Rigidbody kartRigidbody;
    private int currentCheckpoint = 0;
    private int currentLap = 1;
    private float lapStartTime;
    private float bestLapTime = float.MaxValue;
    private float raceTime;
    private bool raceStarted = false;
    private bool raceFinished = false;


    void Start()
    {
        kartRigidbody = GetComponent<Rigidbody>();
        lapStartTime = Time.time;
    }
    void Update()
    {
        if (raceFinished)
            return;

        float throttle = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetButton("Brake") ? braking : 0f;

        // Acceleration and braking
        float accelerationForce = (throttle - brake) * acceleration;
        kartRigidbody.AddForce(transform.forward * accelerationForce);

        // Steering
        float steerForce = steer * steering;
        kartRigidbody.AddTorque(transform.up * steerForce);

        // Calculate speed
        float currentSpeed = kartRigidbody.velocity.magnitude;

        // Speed limit
        if (currentSpeed > maxSpeed)
        {
            kartRigidbody.velocity = (kartRigidbody.velocity.normalized) * maxSpeed;
        }
    }

   /* void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint") && raceStarted)
        {
            int checkpointIndex = int.Parse(other.name);

             Check if the checkpoint was passed in order
            if (checkpointIndex == currentCheckpoint + 1 || (checkpointIndex == 0 && currentCheckpoint == CheckpointManager.Checkpoints.Length - 1))
            {
                currentCheckpoint = checkpointIndex;

                // Check for lap completion
                if (currentCheckpoint == 0)
                {
                    if (currentLap == 1)
                        bestLapTime = Time.time - lapStartTime;
                    else
                        bestLapTime = Mathf.Min(bestLapTime, Time.time - lapStartTime);

                    lapStartTime = Time.time;
                    currentLap++;
                }
            }
        }
    }
*/
    public float GetBestLapTime()
    {
        return bestLapTime;
    }

    public float GetRaceTime()
    {
        return raceTime;
    }

    public bool HasRaceStarted()
    {
        return raceStarted;
    }

    public void StartRace()
    {
        raceStarted = true;
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }
}