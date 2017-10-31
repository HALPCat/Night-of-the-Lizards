using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public class StatModBaseAdd : StatModifier
    {
        public override int Order
        {
            get
            {
                return 2;
            }
        }

        public override int ApplyModifier(int statValue, float modValue)
        {
            return (int)(modValue);
        }

        public StatModBaseAdd(float value) : base(value)
        {

        }

        public StatModBaseAdd(float value, bool stacks) : base(value, stacks)
        {

        }

    }
}
