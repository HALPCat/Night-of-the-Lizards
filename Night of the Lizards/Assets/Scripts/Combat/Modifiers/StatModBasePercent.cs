﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LizardNight
{

    public class StatModBasePercent : StatModifier
    {
        public override int Order
        {
            get
            {
                return 1;
            }
        }

        public override int ApplyModifier(int statValue, float modValue)
        {
            return (int)(statValue * modValue);
        }

        public StatModBasePercent(float value) : base(value)
        {
            
        }

        public StatModBasePercent(float value, bool stacks) : base (value, stacks)
        {
            
        }

    }
}
