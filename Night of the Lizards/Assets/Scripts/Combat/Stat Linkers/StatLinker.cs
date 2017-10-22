using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public abstract class StatLinker : IStatValueChange
    {
        private Stat _stat;

        public event EventHandler OnValueChange;
        
        public StatLinker (Stat stat)
        {
            _stat = stat;

            IStatValueChange iValueChange = _stat as IStatValueChange;
            if (iValueChange != null)
            {
                iValueChange.OnValueChange += OnLinkedStatValueChange;
            }
        }

        public Stat _Stat
        {
            get { return _stat; }
        }

        public abstract int Value { get; }


        private void OnLinkedStatValueChange(object sender, EventArgs args)
        {
            if (OnValueChange != null)
            {
                OnValueChange(this, args);
            }
        }
    }
}
