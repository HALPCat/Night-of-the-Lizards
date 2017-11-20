using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LizardNight
{
    public class SheetUI : MonoBehaviour
    {                 
        public PlayerScript playerScript;
        public PlayerLevel playerLevel;
        public BaseClass playerAttributes;
        
        public Text text1a, text1b, text2a, text2b, text3a, text3b, text4a, text4b, text5a, text5b, text6a, text6b, text7a, text7b;

        private void Awake()
        {
      
        }

        private void OnEnable()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            text1a.text = "Dreamer ";
            text1b.text = "Level: " + playerLevel.Level;
            text2a.text = "Exp: ";
            text2b.text = playerLevel.ExpCurrent + "/" + playerLevel.ExpRequired;
            text3a.text = "Health: ";
            text3b.text = playerScript.GetCurrentHealth() + "/" + playerScript.GetMaxHealth();
            text4a.text = "Strenght: ";
            text4b.text = "" + playerAttributes.GetStat<Attribute>(StatType.Strenght).StatValue;
            text5a.text = "Constitution: ";
            text5b.text = "" + playerAttributes.GetStat<Attribute>(StatType.Constitution).StatValue;
            text6a.text = "Damage";
            text6b.text = playerAttributes.GetStat<Attribute>(StatType.PhysDamage).StatBaseValue + " + " + playerAttributes.GetStat<Attribute>(StatType.PhysDamage).StatModifierValue;
            text7a.text = "Armor: ";
            text7b.text = "" + playerAttributes.GetStat<Attribute>(StatType.ArmorClass).StatValue;
        }

    }
}
