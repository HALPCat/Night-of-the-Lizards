using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    //general stat class
    public class Stat
    {
        private string _statName;
        private int _statBaseValue;

        public string StatName
        {
            get { return _statName; }
            set { _statName = value; }
        }

        public virtual int StatValue
        {
            get { return StatBaseValue; }           
        }

        public virtual int StatBaseValue
        {
            get { return _statBaseValue; }
            set { _statBaseValue = value; }
        }

        public Stat()
        {
            this.StatName = string.Empty;
            this.StatBaseValue = 0;

        }

        public Stat (string name, int value)
        {
            this.StatName = name;
            this.StatBaseValue = value;
        }

        public void SetStat (string name, int value)
        {
            this.StatName = name;
            this.StatBaseValue = value;
        }

        
    }

}
