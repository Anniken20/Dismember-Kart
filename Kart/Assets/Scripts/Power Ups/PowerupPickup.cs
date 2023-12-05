using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class PowerupPickup : MonoBehaviour
    {
        [SerializeField] public KartPowerupManager.PowerUpType _powerUpType;

        [SerializeField] private bool randomize;
        [SerializeField] private bool hasRats;

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
            if (hasRats)
            {
                _powerUpType = (KartPowerupManager.PowerUpType)Random.Range(1,5);
            }
            else
            {
                _powerUpType = (KartPowerupManager.PowerUpType)Random.Range(1,4);
            }
            
            StartCoroutine(RandomPowerUp());
        }
    }
}

