using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public class LinkerBasic : StatLinker
    {
        private float _ratio;

        public override int Value
        {
            get
            {
                return (int)(_Stat.StatValue * _ratio);
            }
        }

        public LinkerBasic(Stat stat, float ratio) : base(stat)
        {
            _ratio = ratio;
        }
       
    }
}
