using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombstoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tombstone;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RespawnTombstone());
    }

    IEnumerator RespawnTombstone()
    {
        yield return new WaitUntil(CheckTombstone);
        yield return new WaitForSeconds(5f);
        tombstone.SetActive(true);
        yield return RespawnTombstone();
    }

    private bool CheckTombstone()
    {
        return !tombstone.activeSelf;
    }
}
