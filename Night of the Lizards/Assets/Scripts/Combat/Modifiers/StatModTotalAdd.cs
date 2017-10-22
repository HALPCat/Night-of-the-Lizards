using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public class StatModTotalAdd : StatModifier
    {
        public override int Order
        {
            get
            {
                return 4;
            }
        }

        public override int ApplyModifier(int statValue, float modValue)
        {
            return (int)(modValue);
        }

        public StatModTotalAdd(float value) : base(value)
        {

        }

        public StatModTotalAdd(float value, bool stacks) : base(value, stacks)
        {

        }

    }
}