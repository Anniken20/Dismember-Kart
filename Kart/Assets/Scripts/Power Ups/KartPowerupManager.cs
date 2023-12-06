
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class KartPowerupManager : MonoBehaviour
    {
        // this script goes on the kart itself and it gets and tracks powerups

        [Header("References")]
        private ArcadeKart arcadeKart;
        private float originalTopSpeed;
        private float originalReverseSpeed;
        private UIPowerupDisplay uiPowerupDisplay;
        private Rigidbody rb;
        public ArcadeKart.StatPowerup speedBoostStats = new ArcadeKart.StatPowerup
        {
            MaxTime = 5
        };


        [Header("Inputs")]
        IInput[] m_Inputs;
        public InputData Input     { get; private set; }
        private bool interactPressed;


        [Header("Manager")]
        private Vector3 originalScale;
        private float timer = 0f;
        public enum PowerUpType
        {
            None,
            Shrink,
            Growth,
            Speed,
            Rat
        }
        [SerializeField] private PowerUpType _powerUpType; 

        [Header("Shrink Powerup")]
        private float shrinkScale = 0.5f;
        private float timeToShrink = 0.5f;
        [SerializeField] private float shrinkTime = 5f;
        private bool isShrunk;
        [SerializeField] private AnimationCurve shrinkAnimCurve;

        [Header("Growth Powerup")]
        private float growthScale = 1.5f;
        private float timeToGrow = 0.5f;
        [SerializeField] private float growTime = 5f;
        private bool isGrown;
        [SerializeField] private AnimationCurve growAnimCurve;

        [Header("Audio")]
        AudioSource audioSource;
        [SerializeField] private AudioClip pickupSound;
        [SerializeField] private AudioClip growSound;
        [SerializeField] private AudioClip shrinkSound;
        [SerializeField] private AudioClip speedSound;
        [SerializeField] private AudioClip speedExpireSound;
        [SerializeField] private AudioClip breakTombstoneSound;
        [SerializeField] private AudioClip ratHit;
        

        [Header("Overhead Check")]
        [SerializeField] private Transform overheadCheckCollider;
        [SerializeField] private LayerMask obstacleLayer;
        public float overheadCheckRadius = 0.5f;

        [Header("Rat Values")]
        [SerializeField] private GameObject ratBastardPrefab;
        [SerializeField] private Transform ratLaunchPosition;
        [SerializeField] private float stunDuration = 3f;
        private int ratCount = 0;
        private int maxRats = 3; // change these values and I'll kill you
        private bool ratLaunchCoolingDown;
        private float ratLaunchCooldownTime = 1f;

        void Awake()
        {
            m_Inputs = GetComponents<IInput>();
            audioSource = GetComponent<AudioSource>();
            arcadeKart = GetComponent<ArcadeKart>();
            rb = GetComponent<Rigidbody>();
        }

        // Start is called before the first frame update
        void Start()
        {
            originalScale = transform.localScale;
            uiPowerupDisplay = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIPowerupDisplay>(); // HEHEHAHEAHAHAEJHEHEHEAHAHAAHA
            originalTopSpeed = arcadeKart.baseStats.TopSpeed;
            originalReverseSpeed = arcadeKart.baseStats.ReverseSpeed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            GatherInputs();
            UsePowerup();
            CheckForSpace();
        }

        void GatherInputs()
        {
            // reset input
            Input = new InputData();

            // gather nonzero input from our sources
            for (int i = 0; i < m_Inputs.Length; i++)
            {
                Input = m_Inputs[i].GenerateInput();

                interactPressed = Input.InteractInput;

                if (interactPressed)
                {
                    Debug.Log("Use current powerup!");
                }
            }
        }

        private void UsePowerup()
        {
            if (interactPressed && !isGrown && !isShrunk)
            {
                switch(_powerUpType)
                {
                    case PowerUpType.Shrink:
                        uiPowerupDisplay.AssBlastUSA();
                        audioSource.PlayOneShot(shrinkSound);
                        StartCoroutine(StartShrink());
                        _powerUpType = PowerUpType.None;
                        break;
                    case PowerUpType.Growth:
                        if (CheckForSpace())
                        {
                            uiPowerupDisplay.AssBlastUSA();
                            audioSource.PlayOneShot(growSound);
                            StartCoroutine(StartGrowth());
                            _powerUpType = PowerUpType.None;
                        }
                        break;
                    case PowerUpType.Speed:
                        uiPowerupDisplay.AssBlastUSA();
                        audioSource.PlayOneShot(speedSound);
                        arcadeKart.AddPowerup(speedBoostStats);
                        StartCoroutine(EndSpeed());
                        _powerUpType = PowerUpType.None;
                        break;
                    case PowerUpType.Rat:
                        if (!ratLaunchCoolingDown)
                        {
                            LaunchRat();
                            ratCount --;
                            if (ratCount == 2) // heheheheheheheheheheHEHAHAHAHAEHAHAHAHEHA
                            {
                                uiPowerupDisplay.EnableRat2Sprite();
                            }
                            else if (ratCount == 1)
                            {
                                uiPowerupDisplay.EnableRat1Sprite();
                            }
                            else if (ratCount < 1)
                            {
                                uiPowerupDisplay.AssBlastUSA();
                                _powerUpType = PowerUpType.None; // I have gone past the point of insanity 
                            }
                        }
                        break;
                }
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Power-Up") && _powerUpType == PowerUpType.None)
            {
                PowerupPickup powerupPickup = collider.GetComponent<PowerupPickup>();
                if (powerupPickup != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                    _powerUpType = powerupPickup._powerUpType;

                    switch(_powerUpType)
                    {
                        case PowerUpType.Shrink:
                        uiPowerupDisplay.EnableShrinkSprite();
                        break;
                        case PowerUpType.Growth:
                        uiPowerupDisplay.EnableGrowSprite();
                        break;
                        case PowerUpType.Speed:
                        uiPowerupDisplay.EnableSpeedSprite();
                        break;
                        case PowerUpType.Rat:
                        ratCount = maxRats;
                        uiPowerupDisplay.EnableRat3Sprite(); // smile
                        break;
                    }
                }
            }


        }

        void OnTriggerStay(Collider collider)
        {
            if (collider.CompareTag("Breakable") && isGrown)
            {
                audioSource.PlayOneShot(breakTombstoneSound);
                collider.gameObject.SetActive(false);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Rat Bastard"))
            {
                audioSource.PlayOneShot(ratHit);
                Destroy(collision.gameObject);
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * (500f * rb.mass));
                StartCoroutine(Damaged());
            }
        }

        IEnumerator Damaged()
        {
            arcadeKart.SetCanMove(false);
            yield return new WaitForSeconds(stunDuration);
            arcadeKart.SetCanMove(true);
            arcadeKart.baseStats.TopSpeed = originalTopSpeed;
            arcadeKart.baseStats.ReverseSpeed = originalReverseSpeed;
        }

        IEnumerator StartShrink()
        {
            if (!isShrunk)
            {
                Vector3 minScale = Vector3.one * shrinkScale;
                timer = 0f;

                while (timer < timeToShrink && !isShrunk)
                {
                    transform.localScale = Vector3.Lerp(originalScale, minScale, shrinkAnimCurve.Evaluate(timer/timeToShrink));
                    timer += Time.deltaTime;
                    yield return null;
                }

                isShrunk = true;
            }

            yield return new WaitForSeconds(shrinkTime);
            yield return new WaitUntil(CheckForSpace);
            audioSource.PlayOneShot(growSound);
            StartCoroutine(EndSizeChange());
        }

        IEnumerator EndSizeChange()
        {
            if (isShrunk || isGrown)
            {
                Vector3 currentScale = transform.localScale;
                timer = 0f;

                while (timer < timeToShrink)
                {
                    transform.localScale = Vector3.Lerp(currentScale, originalScale, shrinkAnimCurve.Evaluate(timer/timeToShrink));
                    timer += Time.deltaTime;
                    yield return null;
                }

                isGrown = false;
                isShrunk = false;
            }
            else
            {
                yield return null;
            }
        }

        IEnumerator StartGrowth()
        {
            Vector3 maxScale = Vector3.one * growthScale;
            timer = 0f;

            while (timer < timeToGrow)
            {
                transform.localScale = Vector3.Lerp(originalScale, maxScale, shrinkAnimCurve.Evaluate(timer/timeToShrink));
                timer += Time.deltaTime;
                yield return null;
            }

            isGrown = true;
            yield return new WaitForSeconds(growTime);
            audioSource.PlayOneShot(shrinkSound);
            StartCoroutine(EndSizeChange());
        }

        IEnumerator EndSpeed()
        {
            yield return new WaitForSeconds(speedBoostStats.MaxTime);
            audioSource.PlayOneShot(speedExpireSound);
        }

        private void LaunchRat()
        {
            ratLaunchCoolingDown = true;
            StartCoroutine(RatLaunchCooldown());
            GameObject rat = Instantiate(ratBastardPrefab) as GameObject;
            rat.transform.position = ratLaunchPosition.position;
            rat.GetComponent<Rigidbody>().velocity = transform.forward * 35f;
        }

        IEnumerator RatLaunchCooldown()
        {
            yield return new WaitForSeconds(ratLaunchCooldownTime);
            ratLaunchCoolingDown = false;
        }

        private bool CheckForSpace()
        {
            if (!Physics.CheckSphere(overheadCheckCollider.position, overheadCheckRadius, obstacleLayer))
            {
                return true;
            }
            return false;
        }


        private void OnDrawGizmos()
        {

        }
    }
}
