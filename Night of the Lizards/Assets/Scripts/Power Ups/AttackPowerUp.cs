using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    public class AttackPowerUp : PowerUp
    {

        public float damageValue;

        protected override void PowerUpPlayer(GameObject player)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();

            playerScript.AttackPowerUp(damageValue);            
        }
    }
}
