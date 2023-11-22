using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Camera mainCamera;

    void Update()
    {
        UpdateSplitScreen();
    }

    void UpdateSplitScreen()
    {
        if (player1 == null || player2 == null || mainCamera == null)
        {
            Debug.LogError("Please assign player transforms and main camera in the inspector.");
            return;
        }

        // Calculate the midpoint between the two players
        Vector3 midpoint = (player1.position + player2.position) / 2f;

        // Adjust the camera position to the midpoint
        mainCamera.transform.position = new Vector3(midpoint.x, midpoint.y, mainCamera.transform.position.z);

        // Calculate the distance between the two players
        float distance = Vector3.Distance(player1.position, player2.position);

        // Adjust the camera size based on the distance between the players
        mainCamera.orthographicSize = distance / 2f;

        // Ensure the camera does not go below a minimum size
        mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize, 5f);
    }
}