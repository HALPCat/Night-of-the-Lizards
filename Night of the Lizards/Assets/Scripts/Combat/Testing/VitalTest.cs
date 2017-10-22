using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LizardNight
{

    public class VitalTest : MonoBehaviour

    {
        private CharacterSheet stats;

        private void Start()
        {
            stats = new BaseClass();

            var health = stats.GetStat<Vital>(StatType.Health);
            health.OnCurrentValueChange += OnStatValueChange;

            DisplayStatValues();

            health.StatCurrentValue -= 75;

            DisplayStatValues();

        }

        void OnStatValueChange(object sender, EventArgs args)
        {
            Vital vital = (Vital)sender;
            if (vital != null)
            {
                Debug.Log(string.Format("Vital {0}'s OnStatValueChange event was triggered", vital.StatName));
            }
        }

        void ForEachEnum<T>(Action<T> action)
        {
            if (action != null)
            {
                var statTypes = Enum.GetValues(typeof(T));
                foreach (var statType in statTypes)
                {
                    action((T)statType);
                }
            }
        }

        void DisplayStatValues()
        {
            ForEachEnum<StatType>((statType) =>
            {
                Stat stat = stats.GetStat((StatType)statType);
                if (stat != null)
                {
                    Vital vital = stat as Vital;
                    if (vital != null)
                    {
                        Debug.Log(string.Format("Stat {0}'s value is {1}/{2}", stat.StatName, vital.StatCurrentValue, stat.StatValue));
                    }
                    else
                    {
                        Debug.Log(string.Format("Stat {0}'s value is {1}", stat.StatName, stat.StatValue));
                    }
                }
            });
        }
    }
}