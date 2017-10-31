using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public interface IStatLinkable
    {
        int StatLinkerValue { get; }

        void AddLinker(StatLinker linker);
        void ClearLinkers();
        void UpdateLinkers();

    }
}
