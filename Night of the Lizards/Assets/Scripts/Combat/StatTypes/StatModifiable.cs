using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LizardNight
{

    public class StatModifiable : Stat, IStatModifiable, IStatValueChange
    {
        private List<StatModifier> _statMods;
        private int _stattModValue;

        public event EventHandler OnValueChange;

        public override int StatValue
        {
            get { return base.StatValue + StatModifierValue; }
        }

        public int StatModifierValue
        {
            get
            {
                return _stattModValue;
            }
        }
        public StatModifiable()
        {
            _stattModValue = 0;
            _statMods = new List<StatModifier>();
        }
    
        protected void TriggerValueChange()
        {
            if (OnValueChange != null)
            {
                OnValueChange(this, null);
            }
        }

        public void AddModifier(StatModifier mod)
        {
            _statMods.Add(mod);
            mod.OnValueChange += OnModValueChange;
        }

        public void RemoveModifier(StatModifier mod)
        {
            _statMods.Remove(mod);
            mod.OnValueChange -= OnModValueChange;

        }

        public void ClearModifiers()
        {
            foreach(var mod in _statMods)
            {
                mod.OnValueChange -= OnModValueChange;

            }
            _statMods.Clear();
        }

        public void UpdateModifiers()
        {
            _stattModValue = 0;

            var orderGroups = _statMods.OrderBy(m => m.Order).GroupBy(m => m.Order);

            foreach (var group in orderGroups)
            {
                float sum = 0, max = 0;
                foreach (var mod in group)
                {
                    if (mod.Stacks == false)
                    {
                        max = mod.Value;
                    }
                    else
                    {
                        sum += mod.Value;
                    }
                }
                _stattModValue += group.First().ApplyModifier(StatBaseValue + _stattModValue, (sum > max) ? sum : max);
            }

            TriggerValueChange();
        }

        public void OnModValueChange (object modifier, EventArgs args)
        {
            UpdateModifiers();
        }

    }
}
