using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHitEffect : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(EffectTimer());
    }

    IEnumerator EffectTimer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
