using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Archetypes
{

    public enum Archetype { None, Supporter, Disabler, Offensive, MInfantry, Infantry, Protector, Brute, Agressive }

    public static Archetype GetArchetype(Equip[] equips)
    {
        Dictionary<Archetype, int> archetypesLevels = new Dictionary<Archetype, int>();

        foreach (Equip equip in equips)
        {
            if (!archetypesLevels.ContainsKey(equip.GetArchetype()))
            {
                archetypesLevels.Add(equip.GetArchetype(), equip.GetLevel());
                break;
            }

            archetypesLevels[equip.GetArchetype()] += equip.GetLevel();
        }

        var archetype = Archetype.None;
        var level = 0;

        foreach (var item in archetypesLevels)
        {
            if (item.Value > level)
            {
                archetype = item.Key;
                level = item.Value;
            }
        }

        return archetype;
    }

}