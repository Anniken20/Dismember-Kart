using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the kart
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from the controller
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement and rotation
        Vector3 movement = transform.forward * verticalInput * speed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // Apply movement and rotation to the Rigidbody
        rb.MovePosition(rb.position + movement);
        rb.MoveRotation(rb.rotation * rotation);
    }
}