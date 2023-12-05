using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerID : MonoBehaviour
{
    [SerializeField] private int playerID = 0;

    private void Awake()
    {
        playerID = Mathf.Clamp(playerID, 1, 2);
    }

    public int GetPlayerID()
    {
        return playerID;
    }

}
