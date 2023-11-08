using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartPowerupManager : MonoBehaviour
{
    // this script goes on the kart itself and it gets and tracks powerups

    private Vector3 originalScale;
    private float timer = 0f;

    [Header("Growth Powerup")]
    private float growthScale = 1.5f;
    private float timeToGrow = 1f;
    private bool isGrown;
    [SerializeField] private AnimationCurve growAnimCurve;

    [Header("Shrink Powerup")]
    private float shrinkScale = 0.5f;
    private float timeToShrink = 0.5f;
    private bool isShrunk;
    [SerializeField] private AnimationCurve shrinkAnimCurve;


    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Power-Up"))
        {
            StartCoroutine(StartShrink());
        }
    }

    IEnumerator StartShrink()
    {
        Vector3 minScale = Vector3.one * shrinkScale;
        timer = 0f;

        while (timer < timeToShrink)
        {
            transform.localScale = Vector3.Lerp(originalScale, minScale, shrinkAnimCurve.Evaluate(timer/timeToShrink));
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        //StartCoroutine(EndShrink());
    }

    IEnumerator EndShrink()
    {
        Vector3 currentScale = transform.localScale;
        timer = 0f;

        while (timer < timeToShrink)
        {
            transform.localScale = Vector3.Lerp(currentScale, originalScale, shrinkAnimCurve.Evaluate(timer/timeToShrink));
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
