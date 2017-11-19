using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{

    public class Attribute : StatModifiable, IStatScalable, IStatLinkable
    {
        private int _statLevelValue;
        private int _statLinkerValue;
        private List<StatLinker> _statLinkers;

       
            public int StatLevelValue
        {
            get { return _statLevelValue; }
        }

        public int StatLinkerValue
        {
            get
            {
                return _statLinkerValue;
            }
        }

        public override int StatBaseValue
        {
            get
            {
               
                return base.StatBaseValue + StatLevelValue + StatLinkerValue;
            }
        }

        public void AddLinker(StatLinker linker)
        {
            _statLinkers.Add(linker);
            linker.OnValueChange += OnLinkerValueChange;
           
        }
        
        public void RemoveLinker(StatLinker linker)
        {
            _statLinkers.Remove(linker);
            linker.OnValueChange -= OnLinkerValueChange;
        }

        public void ClearLinkers()
        {
            foreach (var linker in _statLinkers)
            {
                linker.OnValueChange -= OnLinkerValueChange;
            }
            _statLinkers.Clear();
        }

        public virtual void ScaleStat(int level)
        {
            _statLevelValue = level;
            TriggerValueChange();
        }

        public void UpdateLinkers()
        {
            _statLinkerValue = 0;
            foreach (StatLinker link in _statLinkers)
            {
                _statLinkerValue += link.Value;
                //Debug.Log(_statLinkers.Count);
            }
            TriggerValueChange();
        }

        public Attribute()
        {
            _statLinkers = new List<StatLinker>();
        }

        public void SetStat(string name, int value, LinkerBasic linker)
        {
            StatName = name;
            StatBaseValue = value;
            _statLinkers.Add(linker);
            

        }

        private void OnLinkerValueChange(object linker, EventArgs args)
        {
            UpdateLinkers();
        }
    }
}
