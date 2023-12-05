using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float maxSpeed = 20f;
    public float acceleration = 10f;
    public float braking = 10f;
    public float steering = 5f;
    public float checkpointTolerance = 2.0f;

    [Header("Speed Boost")]
    public float baseSpeed = 10.0f;
    private float currentSpeed;

    private Rigidbody kartRigidbody;
    private int currentCheckpoint = 0;
    private float lapStartTime;
    private float bestLapTime = float.MaxValue;
    private bool raceStarted = false;
    private bool raceFinished = false;

    public CheckpointManager checkpointManager;
    private int currentCheckpointIndex = 0;
    private int currentLap = 1;

    private void Start()
    {
        kartRigidbody = GetComponent<Rigidbody>();
        lapStartTime = Time.time;
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        if (raceFinished)
            return;

        HandleInput();
        LimitSpeed();
    }

    private void HandleInput()
    {
        float throttle = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetButton("Brake") ? braking : 0f;

        float accelerationForce = (throttle - brake) * acceleration;
        kartRigidbody.AddForce(transform.forward * accelerationForce);

        float steerForce = steer * steering;
        kartRigidbody.AddTorque(transform.up * steerForce);
    }

    private void LimitSpeed()
    {
        float currentSpeed = kartRigidbody.velocity.magnitude;

        if (currentSpeed > maxSpeed)
        {
            kartRigidbody.velocity = (kartRigidbody.velocity.normalized) * maxSpeed;
        }
    }

    public void ApplySpeedBoost(float speedBoostAmount, float duration)
    {
        currentSpeed += speedBoostAmount;
        StartCoroutine(RemoveSpeedBoostAfterDuration(duration));
    }

    private IEnumerator RemoveSpeedBoostAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentSpeed = baseSpeed;
    }

    public void OnCheckpointReached(Transform checkpointTransform)
    {
        if (raceStarted)
        {
            int expectedCheckpointIndex = (currentCheckpointIndex + 1) % checkpointManager.Checkpoints.Count;

            if (checkpointTransform == checkpointManager.Checkpoints[expectedCheckpointIndex])
            {
                currentCheckpointIndex = expectedCheckpointIndex;
                CheckpointPassed(); // Handle checkpoint logic here
            }
        }
    }

    private void CheckpointPassed()
    {
        if (currentCheckpointIndex == 0)
        {
            if (currentLap == 1)
                bestLapTime = Time.time - lapStartTime;
            else
                bestLapTime = Mathf.Min(bestLapTime, Time.time - lapStartTime);

            lapStartTime = Time.time;
            currentLap++;
        }
    }

    public float GetBestLapTime()
    {
        return bestLapTime;
    }

    public float GetRaceTime()
    {
        return Time.time - lapStartTime;
    }

    public bool HasRaceStarted()
    {
        return raceStarted;
    }

    public bool IsRaceFinished()
    {
        return raceFinished;
    }

    public void FinishRace()
    {
        raceFinished = true;
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }
}