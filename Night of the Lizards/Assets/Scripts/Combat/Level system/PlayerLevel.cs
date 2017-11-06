using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public class PlayerLevel : CharLevel
    {
        //need to figure out the rate at which required experience to level raises
        public override int GetExpRequiredForLevel(int level)
        {
            return (int)(Math.Pow(Level, 2f) * 100) + 100;
        }
             
    }
}
