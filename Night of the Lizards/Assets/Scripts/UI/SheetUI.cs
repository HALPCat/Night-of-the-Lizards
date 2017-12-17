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
        
        public Text text1b, text2b, text3b,  text4b, text5b, text6b, text7b;

        private void Awake()
        {
      
        }

        private void OnEnable()
        {
            UpdateUI();
            playerScript.paused = true;
        }

        private void OnDisable()
        {
            playerScript.paused = false;
        }

        private void UpdateUI()
        {
            
            text1b.text = playerLevel.Level.ToString();
           
            text2b.text = playerLevel.ExpCurrent + "/" + playerLevel.ExpRequired;
           
            text3b.text = playerScript.GetCurrentHealth() + "/" + playerScript.GetMaxHealth();
            
            text4b.text = "" + playerAttributes.GetStat<Attribute>(StatType.Strenght).StatValue;
            
            text5b.text = "" + playerAttributes.GetStat<Attribute>(StatType.Constitution).StatValue;
           
            text6b.text = playerAttributes.GetStat<Attribute>(StatType.PhysDamage).StatBaseValue + " + " + playerAttributes.GetStat<Attribute>(StatType.PhysDamage).StatModifierValue;
            
            text7b.text = "" + playerAttributes.GetStat<Attribute>(StatType.ArmorClass).StatValue;
        }

    }
}
