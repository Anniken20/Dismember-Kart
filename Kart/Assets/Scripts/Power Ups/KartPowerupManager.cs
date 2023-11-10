
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

        [Header("Overhead Check")]
        [SerializeField] private Transform overheadCheckCollider;
        [SerializeField] private LayerMask obstacleLayer;
        public float overheadCheckRadius = 0.5f;

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
            if (interactPressed)
            {
                switch(_powerUpType)
                {
                    case PowerUpType.Shrink:
                        StartCoroutine(StartShrink());
                        _powerUpType = PowerUpType.None;
                        break;
                    case PowerUpType.Growth:
                        if (CheckForSpace())
                        {
                            StartCoroutine(StartGrowth());
                            _powerUpType = PowerUpType.None;
                        }
                        break;
                    case PowerUpType.Speed:
                        break;
                }
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

            yield return new WaitForSeconds(2f);
            /*while (isShrunk)
            {
                if(CheckForSpace())
                {
                    StartCoroutine(EndSizeChange());
                }
            }*/
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
            yield return new WaitForSeconds(2f);
            StartCoroutine(EndSizeChange());
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
            /*// draws gizmo for overhead checker
            if (CheckForSpace())
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);*/
        }
    }
}
