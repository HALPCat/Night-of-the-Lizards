using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight
{
    public abstract class StatModifier
    {
        private float _value;

        public event EventHandler OnValueChange;

        public abstract int Order { get; }

        public bool Stacks { get; set; }

        public float Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;

                    if (OnValueChange != null)
                    {
                        OnValueChange(this, null);
                    }
                }
            }
        }


        public StatModifier(float value)
        {
            _value = value;
            Stacks = false;
        }

        public StatModifier(float value, bool stacks)
        {
            _value = value;
            Stacks = stacks;
        }

        public abstract int ApplyModifier(int statValue, float modValue);
    }
}
