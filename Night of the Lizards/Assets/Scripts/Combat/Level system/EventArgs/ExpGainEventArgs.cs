using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LizardNight
{

    public class ExpGainEventArgs : EventArgs
    {
        public int ExpGained { get; private set; }

        public ExpGainEventArgs(int expGained)
        {
            ExpGained = expGained;
        }
    }
}
