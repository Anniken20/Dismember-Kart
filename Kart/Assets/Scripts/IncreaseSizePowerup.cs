using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSizePowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        var rb = collider.attachedRigidbody;

        if (collider.tag == "Player")
        {
            collider.gameObject.transform.localScale += new Vector3 (5,5,5);
        }

    }
}
