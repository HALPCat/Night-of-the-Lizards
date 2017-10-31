using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    //basic class for characters
    //class as in rpg not C#
    public class BaseClass : CharacterSheet
    {
        //starting stats, for control in unity
        [SerializeField]
        int Health = 20;
        [SerializeField]
        int Constitution = 5;
        [SerializeField]
        int Damage = 1;
        [SerializeField]
        int Strenght = 5;
        
        protected override void ConfigureStats()
        {
            var constitution = CreateOrGetStat<Attribute>(StatType.Constitution);
            constitution.SetStat("Constitution", Constitution);

            var strenght = CreateOrGetStat<Attribute>(StatType.Strenght);
            strenght.SetStat("Strenght", Strenght);

            var health = CreateOrGetStat<Vital>(StatType.Health);
            health.SetStat("Health", Health);
            health.AddLinker(new LinkerBasic(CreateOrGetStat< Attribute > (StatType.Constitution), 5));
            health.UpdateLinkers();
            health.SetCurrentValueToMax();

            var physDamage = CreateOrGetStat<Attribute>(StatType.PhysDamage);
            physDamage.SetStat("Physical Damage", 1);
            physDamage.AddLinker(new LinkerBasic(CreateOrGetStat<Attribute>(StatType.Strenght), 3));
            physDamage.UpdateLinkers();
          

                     
        }



        //protected override void ConfigureStats()
        //{
        //    var constitution = CreateOrGetStat<Attribute>(StatType.Constitution);
        //    constitution.SetStat("Constitution", 10);

        //    var wisdom = CreateOrGetStat<Attribute>(StatType.Wisdom);
        //    wisdom.SetStat("Wisdom", 5);

        //    var health = CreateOrGetStat<Vital>(StatType.Health);
        //    health.SetStat("Health", 100);
        //    health.AddLinker(new LinkerBasic(CreateOrGetStat<Attribute>(StatType.Constitution), 10));
        //    health.UpdateLinkers();
        //    health.SetCurrentValueToMax();

        //    var mana = CreateOrGetStat<Vital>(StatType.Mana);
        //    mana.SetStat("Mana", 10);
        //    mana.AddLinker(new LinkerBasic(CreateOrGetStat<Attribute>(StatType.Wisdom), 10));
        //    mana.UpdateLinkers();
        //    mana.SetCurrentValueToMax();

        //}

    }
}
