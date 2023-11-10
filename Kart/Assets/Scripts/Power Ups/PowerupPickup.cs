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

        }

        IEnumerator RandomPowerUp()
        {
            return null;
        }
    }
}

