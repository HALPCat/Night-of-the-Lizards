using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LizardNight
{

    public abstract class CharLevel : MonoBehaviour
    {
        [SerializeField]
        private int _level = 1;
        [SerializeField]
        private int _levelMin = 1;
        [SerializeField]
        private int _levelMax = 99;

        private int _expCurrent = 0;

        private int _expRequired = 0;

        public event EventHandler<ExpGainEventArgs> OnCharExpGain;
        public event EventHandler<LevelChangeEventArgs> OnCharLevelChange;
        public event EventHandler<LevelChangeEventArgs> OnCharLevelUp;
        public event EventHandler<LevelChangeEventArgs> OnCharLevelDown;


        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int LevelMin
        {
            get { return _levelMin; }
            set { _levelMin = value; }
        }

        public int LevelMax
        {
            get { return _levelMax; }
            set { _levelMax = value; }
        }

        public int ExpCurrent
        {
            get { return _expCurrent; }
            set { _expCurrent = value; }
        }

        public int ExpRequired
        {
            get { return _expRequired; }
            set { _expRequired = value; }
        }

        public abstract int GetExpRequiredForLevel(int level);

        private void Awake()
        {
            ExpRequired = GetExpRequiredForLevel(Level);

        }

        public void ModifyExp (int amount)
        {
            ExpCurrent += amount;

            if (OnCharExpGain != null)
            {
                OnCharExpGain(this, new ExpGainEventArgs(amount));
            }

            ChechCurrentEXp();
        }

        public void SetCurrentExp (int value)
        {
            int expGained = value - ExpCurrent;

            ExpCurrent = value;

            if (OnCharExpGain != null)
            {
                OnCharExpGain(this, new ExpGainEventArgs(expGained));
            }

            ChechCurrentEXp();
        }

        public void ChechCurrentEXp()
        {
            int oldLevel = Level;

            InternalCheckCurrentExp();

            if (oldLevel != Level && OnCharLevelChange != null)
            {
                OnCharLevelChange(this, new LevelChangeEventArgs(Level, oldLevel));
            }
        }

        private void InternalCheckCurrentExp()
        {
            while(true)
            {
                if (ExpCurrent >= ExpRequired)
                {
                    ExpCurrent -= ExpRequired;
                    InternalIncreaseCurrentLevel();
                }
                else if (ExpCurrent < 0)
                {
                    ExpCurrent += GetExpRequiredForLevel(Level - 1);
                }
                else
                {
                    break;
                }
            }
        }

        public void IncreaseCurrentLevel()
        {
            int oldLevel = Level;

            InternalIncreaseCurrentLevel();

            if (oldLevel != Level && OnCharLevelChange != null)
            {
                OnCharLevelChange(this, new LevelChangeEventArgs(Level, oldLevel));
            }
        }
        private void InternalIncreaseCurrentLevel()
        {
            int oldLevel = Level++;

            if (Level > LevelMax)
            {
                Level = LevelMax;
                ExpCurrent = GetExpRequiredForLevel(Level);
            }

            ExpRequired = GetExpRequiredForLevel(Level);

            if (oldLevel != Level && OnCharLevelUp != null)
            {
                OnCharLevelUp(this, new LevelChangeEventArgs(Level, oldLevel));
            }
        }
        public void DecreaseCurrentLevel()
        {
            int oldLevel = Level;

            InternalDecreaseCurrentLevel();

            if (oldLevel != Level && OnCharLevelChange != null)
            {
                OnCharLevelChange(this, new LevelChangeEventArgs(Level, oldLevel));
            }
        }

        private void InternalDecreaseCurrentLevel()
        {
            int oldLevel = Level--;

            if (Level < LevelMin)
            {
                Level = LevelMin;
                ExpCurrent = 0;
            }

            ExpRequired = GetExpRequiredForLevel(Level);


            if (oldLevel != Level && OnCharLevelDown != null)
            {
                OnCharLevelDown(this, new LevelChangeEventArgs(Level, oldLevel));
            }
        }

        public void SetLevel (int targetLevel)
        {
            SetLevel(targetLevel, true);
        }

        public void SetLevel(int targetLevel, bool clearExp)
        {
            int oldLevel = Level;

            Level = targetLevel;
            ExpRequired = GetExpRequiredForLevel(Level);

            if (clearExp)
            {
                SetCurrentExp(0);
            }else
            {
                InternalCheckCurrentExp();
            }

            if (oldLevel != Level && OnCharLevelChange != null)
            {
                OnCharLevelChange(this, new LevelChangeEventArgs(Level, oldLevel));
            }

        }


    }
}
