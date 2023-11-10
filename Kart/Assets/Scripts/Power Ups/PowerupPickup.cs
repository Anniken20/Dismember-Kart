using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class PowerupPickup : MonoBehaviour
    {
        [SerializeField] public KartPowerupManager.PowerUpType _powerUpType;

        [SerializeField] private bool randomize;

        void Start()
        {
            if (randomize)
            {
                StartCoroutine(RandomPowerUp());
            }
        }

        IEnumerator RandomPowerUp()
        {
            yield return new WaitForSeconds(1f);
            _powerUpType = (KartPowerupManager.PowerUpType)Random.Range(1,4);
            StartCoroutine(RandomPowerUp());
        }
    }
}

