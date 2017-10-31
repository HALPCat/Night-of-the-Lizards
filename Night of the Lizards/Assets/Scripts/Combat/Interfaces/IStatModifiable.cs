using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    public interface IStatModifiable
    {
        int StatModifierValue { get; }

        void AddModifier(StatModifier mod);
        void RemoveModifier(StatModifier mod);
        void ClearModifiers();
        void UpdateModifiers();
    }

}
