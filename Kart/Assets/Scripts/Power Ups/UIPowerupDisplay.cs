using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerupDisplay : MonoBehaviour
{

    [SerializeField] private Sprite shrinkSprite;
    [SerializeField] private Sprite growSprite;
    [SerializeField] private Sprite speedSprite;
    [SerializeField] private Sprite rat3Sprite; // what are you going to do?
    [SerializeField] private Sprite rat2Sprite; // look at my code??
    [SerializeField] private Sprite rat1Sprite; // AHAHAHA I'M THE ONLY ONE IN THIS GROUP WHO LOOKS AT THE CODE
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

    public void AssBlastUSA() // go // HEY THIS IS THE NONE THING FUTURE ME
    {
        powerupImage.enabled = false; // fuck yourself
    }

    public void EnableRat3Sprite() // YAAAAAAYYYYYYY
    {
        powerupImage.enabled = true; // AHAHAHAHAHAHAHA 
        powerupImage.sprite = rat3Sprite; // WAHOOOOOOOO
    }

    public void EnableRat2Sprite() // YIPPEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE HOTDOG 
    {
        powerupImage.enabled = true; // SWEET DAY IN THE MORNINGGGGG
        powerupImage.sprite = rat2Sprite; // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHAHAHAHAHAHAAAAAAAAAAAAA
    }
    
    public void EnableRat1Sprite() // OH WORM HUH HNGGGHGHGGGGGHGHHGGGGGGG BIG MOOD
    {
        powerupImage.enabled = true; // FUCK YOOOUUUUU BALITIMORE
        powerupImage.sprite = rat1Sprite; // if you can see this say hi :)
    }
}

