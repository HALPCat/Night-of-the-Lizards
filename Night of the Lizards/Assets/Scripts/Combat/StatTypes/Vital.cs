using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LizardNight
{

    public class Vital : Attribute, IStatCurrentValueChange
    {
        private int _statCurrentValue;

        public event EventHandler OnCurrentValueChange;

        public int StatCurrentValue
        {
            get
            {
                if (_statCurrentValue > StatValue)
                {
                    _statCurrentValue = StatValue;
                }
                else if (_statCurrentValue < 0)
                {
                    _statCurrentValue = 0;
                }
                return _statCurrentValue;
            }
            set
            {
                if (_statCurrentValue != value)
                {
                    _statCurrentValue = value;
                    triggerCurrentValueChange();
                }
            }
        }

        public Vital()
        {
            _statCurrentValue = 0;
        }

        public void SetCurrentValueToMax()
        {
            _statCurrentValue = StatValue;
        }

        public void triggerCurrentValueChange()
        {
            if (OnCurrentValueChange != null)
            {
                OnCurrentValueChange(this, null);
            }
        }
    }
}
