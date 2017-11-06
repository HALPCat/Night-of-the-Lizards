using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace LizardNight
{
    public class ExperienceBar : MonoBehaviour
    {
       
        public PlayerScript character;

        public RectTransform expBarArea;
        public RectTransform expBarFill;

        public Text expBarValues;


        // Use this for initialization
        void Awake()
        {            
            
            
        }

        private void Start()
        {
            character.CharLevel.OnCharExpGain += OnExpGain;
            character.CharLevel.OnCharLevelChange += OnLevelChange;
            UpdateUI();
        }
        void OnExpGain(object sender, ExpGainEventArgs args)
        {
            UpdateUI();
        }

        void OnLevelChange(object sender, LevelChangeEventArgs args)
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            float expPercent = Mathf.Clamp((float)character.CharLevel.ExpCurrent / (float)character.CharLevel.ExpRequired, 0f, 1f);

            float newRightOffset = -expBarArea.rect.width + expBarArea.rect.width * expPercent;

            expBarFill.offsetMax = new Vector2(newRightOffset, expBarFill.offsetMax.y);

            expBarValues.text = string.Format("{0} / {1} (Level {2})", character.CharLevel.ExpCurrent, character.CharLevel.ExpRequired, character.CharLevel.Level);

        }
    }
}
