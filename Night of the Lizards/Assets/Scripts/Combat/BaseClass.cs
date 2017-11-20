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
        [SerializeField]
        int Armor = 0;

        protected override void ConfigureStats()
        {
            var constitution = CreateOrGetStat<Attribute>(StatType.Constitution);
            constitution.SetStat("Constitution", Constitution);

            var strenght = CreateOrGetStat<Attribute>(StatType.Strenght);
            strenght.SetStat("Strenght", Strenght);

            var health = CreateOrGetStat<Vital>(StatType.Health);
            health.SetStat("Health", Health);
            health.AddLinker(new LinkerBasic(CreateOrGetStat<Attribute>(StatType.Constitution), 5));
            health.UpdateLinkers();
            health.SetCurrentValueToMax();

            var physDamage = CreateOrGetStat<Attribute>(StatType.PhysDamage);
            physDamage.SetStat("Physical Damage", 1);
            physDamage.AddLinker(new LinkerBasic(CreateOrGetStat<Attribute>(StatType.Strenght), 2));
            physDamage.UpdateLinkers();

            var armor = CreateOrGetStat<Attribute>(StatType.ArmorClass);
            armor.SetStat("Armor", Armor);


        }

        public void LevelUpStats(int level)
        {
            foreach (var key in StatDict.Keys)
            {
                
            }
        }
      
    }
}
