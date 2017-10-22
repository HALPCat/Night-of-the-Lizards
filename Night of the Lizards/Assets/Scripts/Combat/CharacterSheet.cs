using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{
    //organizes stats, initialises, updates them, etc..
    public class CharacterSheet : MonoBehaviour
    {
        private Dictionary<StatType, Stat> _statDict;

        public Dictionary<StatType, Stat> StatDict
        {
            get
            {
                if (_statDict == null)
                {
                    _statDict = new Dictionary<StatType, Stat>();
                }
                return _statDict;
            }

        }

        private void Awake()
        {
            ConfigureStats();
        }

        protected virtual void ConfigureStats()
        {

        }

        public bool ContainStat(StatType statType)
        {
            return StatDict.ContainsKey(statType);
        }

        public Stat GetStat(StatType statType)
        {
            if (ContainStat(statType))
            {
                return StatDict[statType];
            }
            return null;
        }

        public T GetStat<T>(StatType type) where T : Stat
        {
            return GetStat(type) as T;
        }

        protected T CreateStat<T>(StatType statType) where T : Stat
        {
            T stat = System.Activator.CreateInstance<T>();
            StatDict.Add(statType, stat);
            return stat;

        }

        protected T CreateOrGetStat<T>(StatType statType) where T : Stat
        {
            T stat = GetStat<T>(statType);
            if (stat == null)
            {
                stat = CreateStat<T>(statType);
            }
            return stat;
        }

        public void AddModifier(StatType target, StatModifier mod )
        {
            AddModifier(target, mod, false);
        }

        public void AddModifier(StatType target, StatModifier mod, bool update)
        {
            if (ContainStat(target))
            {
                var modStat = GetStat(target) as IStatModifiable;
                if (modStat != null)
                {
                    modStat.AddModifier(mod);
                    if (update == true)
                    {
                        modStat.UpdateModifiers();
                    }
                }
                else
                    Debug.Log("Trying to add a modifier to non modofiable stat \"" + target.ToString() + "\"");
            }
            else
            {
                Debug.Log("Trying to add stat modifier to \"" + target.ToString() + "\", but the character sheet does not contain the stat");
            }
        }

        public void RemoveModifier(StatType target, StatModifier mod)
        {
            RemoveModifier(target, mod, false);
        }

        public void RemoveModifier(StatType target, StatModifier mod, bool update)
        {
            if (ContainStat(target))
            {
                var modStat = GetStat(target) as IStatModifiable;
                if (modStat != null)
                {
                    modStat.RemoveModifier(mod);
                    if (update == true)
                    {
                        modStat.UpdateModifiers();
                    }
                }
                else
                    Debug.Log("Trying to remove a modifier from non modifiable stat \"" + target.ToString() + "\"");
            }
            else
            {
                Debug.Log("Trying to remove stat modifier from \"" + target.ToString() + "\", but the CharacterSheet does not contain the stat");
            }
        }

        public void ClearAllStatModifiers()
        {
            ClearAllStatModifiers(false);
        }

        public void ClearAllStatModifiers(bool update)
        {
            foreach(var key in StatDict.Keys )
            {
                ClearStatModifier(key, update);
            }
        }

        public void ClearStatModifier(StatType target)
        {
            ClearStatModifier(target, false);
        }

        public void ClearStatModifier(StatType target, bool update)
        {
            if (ContainStat(target))
            {
                var modStat = GetStat(target) as IStatModifiable;
                if (modStat != null)
                {
                    modStat.ClearModifiers();
                    if (update == true)
                    {
                        modStat.UpdateModifiers();
                    }
                }
                else
                    Debug.Log("Trying to clear a modifier from non modifiable stat \"" + target.ToString() + "\"");
            }
            else
            {
                Debug.Log("Trying to clear stat modifier from \"" + target.ToString() + "\", but the CharacterSheet does not contain the stat");
            }
        }

        public void UpdateAllStatModifiers()
        {
            foreach (var key in StatDict.Keys)
            {
                UpdateStatModifier(key);
            }
        }

        public void UpdateStatModifier(StatType target)
        {
            if (ContainStat(target))
            {
                var modStat = GetStat(target) as IStatModifiable;
                if (modStat != null)
                {
                    modStat.UpdateModifiers();
                  
                }
                else
                    Debug.Log("Trying to clear a modifier from non modifiable stat \"" + target.ToString() + "\"");
            }
            else
            {
                Debug.Log("Trying to clear stat modifier from \"" + target.ToString() + "\", but the CharacterSheet does not contain the stat");
            }
        }
    }
}


