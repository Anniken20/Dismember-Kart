using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    void Update()
    {
        // Player 2 input controls
        float horizontalInput = Input.GetAxis("Player2_Horizontal");
        float verticalInput = Input.GetAxis("Player2_Vertical");

        // Move the player
        MovePlayer(horizontalInput, verticalInput);
    }

    void MovePlayer(float horizontal, float vertical)
    {
        // Calculate movement and rotation
        Vector3 movement = new Vector3(horizontal * moveSpeed * Time.deltaTime, vertical * moveSpeed * Time.deltaTime, 0f);
        transform.Translate(movement, Space.World);

        // Rotate the player based on horizontal input
        float rotationAmount = -horizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }
}