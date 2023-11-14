using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float raycastDistance = 1.1f; // Adjust the distance of the raycast to match the height of the object

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            // Object is grounded
            Debug.Log("Grounded");
        }
        else
        {
            // Object is in the air
            Debug.Log("Not Grounded");
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            return true;
        }
        return false;
    }
}