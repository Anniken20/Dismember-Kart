using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerupDisplay : MonoBehaviour
{

    [SerializeField] private Sprite shrinkSprite;
    [SerializeField] private Sprite growSprite;
    [SerializeField] private Sprite speedSprite;
    [SerializeField] private Image powerupImage;

    void Start()
    {
        powerupImage.enabled = false;
    }

    public void EnableGrowSprite() // I
    {
        powerupImage.enabled = true; // am
        powerupImage.sprite = growSprite; // extremely
    }

    public void EnableShrinkSprite() // tired
    {
        powerupImage.enabled = true; // and
        powerupImage.sprite = shrinkSprite; // sick
    }

    public void EnableSpeedSprite() // of 
    {
        powerupImage.enabled = true; // this
        powerupImage.sprite = speedSprite; // project
    }

    public void AssBlastUSA() // go
    {
        powerupImage.enabled = false; // fuck yourself
    }
}

