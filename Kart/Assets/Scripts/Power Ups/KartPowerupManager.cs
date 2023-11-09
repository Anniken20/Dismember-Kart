
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class KartPowerupManager : MonoBehaviour
    {
        // this script goes on the kart itself and it gets and tracks powerups


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
            Speed
        }

        [SerializeField] private PowerUpType _powerUpType; 

        [Header("Shrink Powerup")]
        private float shrinkScale = 0.5f;
        private float timeToShrink = 0.5f;
        private bool isShrunk;
        [SerializeField] private AnimationCurve shrinkAnimCurve;

        [Header("Growth Powerup")]
        private float growthScale = 1.5f;
        private float timeToGrow = 1f;
        private bool isGrown;
        [SerializeField] private AnimationCurve growAnimCurve;

        void Awake()
        {
            m_Inputs = GetComponents<IInput>();
        }

        // Start is called before the first frame update
        void Start()
        {
            originalScale = transform.localScale;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            GatherInputs();
            UsePowerup();
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
            if (interactPressed)
            {
                switch(_powerUpType)
                {
                    case PowerUpType.Shrink:
                        StartCoroutine(StartShrink());
                        break;
                    case PowerUpType.Growth:
                        StartCoroutine(StartGrowth());
                        break;
                    case PowerUpType.Speed:
                        break;
                }

                _powerUpType = PowerUpType.None;
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Power-Up"))
            {
                PowerupPickup powerupPickup = collider.GetComponent<PowerupPickup>();
                if (powerupPickup != null)
                {
                    _powerUpType = powerupPickup._powerUpType;
                }
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

            isShrunk = true;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EndSizeChange());
        }

        IEnumerator EndSizeChange()
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
            yield return new WaitForSeconds(2f);
            StartCoroutine(EndSizeChange());
        }

        private bool CheckForSpace()
        {
            return false;
        }
    }
}
