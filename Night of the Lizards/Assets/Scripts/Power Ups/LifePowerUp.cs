using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    public class LifePowerUp : PowerUp
    {

        [SerializeField]
        int healthToHeal;

        protected override void PowerUpPlayer(GameObject player)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();

            playerScript.HealDamage(healthToHeal);
        }
     
    }
}
