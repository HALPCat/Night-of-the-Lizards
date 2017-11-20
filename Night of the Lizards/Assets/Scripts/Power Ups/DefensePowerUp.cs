using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight
{

    public class DefensePowerUp : PowerUp
    {
        public float armorLevel;

        protected override void PowerUpPlayer(GameObject player)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();

            playerScript.DefensePowerUp(armorLevel);
        }
    }
}
