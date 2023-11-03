using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KartMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody kartRigidbody;

    private void Start()
    {
        kartRigidbody = GetComponent<Rigidbody>();
        kartRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        kartRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        // Get input for movement and turning
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Calculate movement and rotation
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, turnInput * turnSpeed * Time.deltaTime, 0);

        // Apply movement and rotation to the kart
        kartRigidbody.MovePosition(kartRigidbody.position + movement);
        kartRigidbody.MoveRotation(kartRigidbody.rotation * rotation);
    }
}