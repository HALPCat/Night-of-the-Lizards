using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public interface IStatValueChange
    {
        event EventHandler OnValueChange;

    }
}
