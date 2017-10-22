using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    //basic class for characters
    //class as in rpg not C#
    public class EnemyOne : CharacterSheet
    {
        //starting stats, for control in unity
        [SerializeField]
        int Health = 10;
        [SerializeField]
        int Constitution = 2;
        [SerializeField]
        int Damage = 1;
        [SerializeField]
        int Strenght = 2;

        protected override void ConfigureStats()
        {
            var constitution = CreateOrGetStat<Attribute>(StatType.Constitution);
            constitution.SetStat("Constitution", Constitution);

            var strenght = CreateOrGetStat<Attribute>(StatType.Strenght);
            constitution.SetStat("Strenght", Strenght);

            var health = CreateOrGetStat<Vital>(StatType.Health);
            health.SetStat("Health", Health);
            health.AddLinker(new LinkerBasic(CreateOrGetStat<Attribute>(StatType.Constitution), 5));
            health.UpdateLinkers();
            health.SetCurrentValueToMax();

            var physDamage = CreateOrGetStat<Attribute>(StatType.PhysDamage);
            physDamage.SetStat("Physical Damage", Damage);
            physDamage.AddLinker(new LinkerBasic(CreateOrGetStat<Attribute>(StatType.Strenght), 5));
            physDamage.UpdateLinkers();

            Debug.Log("Enemy damage is " + physDamage.StatBaseValue);




        }
    }
}
