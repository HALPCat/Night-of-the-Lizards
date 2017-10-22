using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LizardNight
{

    public class TestStats : MonoBehaviour
    {
        private CharacterSheet stats;

        private void Start()
        {
            stats = GetComponent<BaseClass>();

            DisplayStatValues();

            var health = stats.GetStat<Attribute>(StatType.Health);

            health.AddModifier(new StatModBasePercent(1.0f, false));
            health.AddModifier(new StatModBaseAdd(50f));
            health.AddModifier(new StatModTotalPercent(1));

            health.UpdateModifiers();


            stats.GetStat<Attribute>(StatType.Constitution).ScaleStat(5);
            stats.GetStat<Attribute>(StatType.Strenght).ScaleStat(10);


            DisplayStatValues();
           
        }

        void ForEachEnum<T>(Action<T> action)
        {
            if (action!= null)
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
                    Debug.Log(string.Format("Stat {0}'s value is {1}", stat.StatName, stat.StatValue));
            });
        }
    }
}
