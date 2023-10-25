using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GhostTransform
{
    public Vector3 position;
    public Quaternion rotation;

    public GhostTransform(Transform transform)
    {
        this.position = transform.position;
        this.rotation = transform.rotation;
    }
}


public class GhostManager : MonoBehaviour
{
    public Transform kart;
    public Transform ghostKart;

    public bool recording;
    public bool playing;

    private List<GhostTransform> recordedGhostTransform = new List<GhostTransform>();
    private GhostTransform lastRecordedGhostTransform; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recording == true)
        {
            if(kart.position != lastRecordedGhostTransform.position || kart.rotation != lastRecordedGhostTransform.rotation)
            {
                var newGhostTransform = new GhostTransform(kart);
                recordedGhostTransform.Add(newGhostTransform);

                lastRecordedGhostTransform = newGhostTransform;
            }
        }

        if(playing == true)
        {
            Play();
        }

    }

    void Play()
    {
        ghostKart.gameObject.SetActive(true);
        StartCoroutine(StartGhost());
        playing = false;

    }

    IEnumerator StartGhost()
    {
        for(int i = 0; i < recordedGhostTransform.Count; i++)
        {
            ghostKart.position = recordedGhostTransform[i].position;
            ghostKart.rotation = recordedGhostTransform[i].rotation;
            yield return new WaitForFixedUpdate();
        }
    }
}