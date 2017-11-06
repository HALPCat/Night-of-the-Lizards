using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LizardNight
{
    
    public class LevelChangeEventArgs : EventArgs
    {
        public int NewLevel { get; private set; }  
        public int OldLevel { get; private set; }

        public LevelChangeEventArgs(int newLevel, int oldLevel)
        {
            NewLevel = newLevel;
            OldLevel = oldLevel;
        }
        
    }
}
