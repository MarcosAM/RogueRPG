using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archetypes : MonoBehaviour
{
    [SerializeField] protected Equip[] supportersEquips;
    [SerializeField] protected Equip[] disablersEquips;
    [SerializeField] protected Equip[] mOffensiveEquips;
    [SerializeField] protected Equip[] offensiveEquips;
    [SerializeField] protected Equip[] mInfantryEquips;
    [SerializeField] protected Equip[] infantryEquips;
    [SerializeField] protected Equip[] bruteEquips;
    [SerializeField] protected Equip[] agressiveEquips;

    [SerializeField] RuntimeAnimatorController[] animators;
    static Archetypes instance;

    private void Awake()
    {
        instance = this;
    }

    public enum Archetype { None, Supporter, Disabler, MOffensive, Offensive, MInfantry, Infantry, Brute, Agressive }

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

    public static Equip GetMomentumEquip(Archetype archetype, int level)
    {
        try
        {
            switch (archetype)
            {
                case Archetype.None:
                case Archetype.Supporter:
                    return instance.supportersEquips[level];
                case Archetype.Disabler:
                    return instance.disablersEquips[level];
                case Archetype.MOffensive:
                    return instance.mOffensiveEquips[level];
                case Archetype.Offensive:
                    return instance.offensiveEquips[level];
                case Archetype.MInfantry:
                    return instance.mInfantryEquips[level];
                case Archetype.Infantry:
                    return instance.infantryEquips[level];
                case Archetype.Brute:
                    return instance.bruteEquips[level];
                case Archetype.Agressive:
                    return instance.agressiveEquips[level];
            }
        }
        catch
        {
            switch (archetype)
            {
                case Archetype.None:
                case Archetype.Supporter:
                    return instance.supportersEquips[0];
                case Archetype.Disabler:
                    return instance.disablersEquips[0];
                case Archetype.MOffensive:
                    return instance.mOffensiveEquips[0];
                case Archetype.Offensive:
                    return instance.offensiveEquips[0];
                case Archetype.MInfantry:
                    return instance.mInfantryEquips[0];
                case Archetype.Infantry:
                    return instance.infantryEquips[0];
                case Archetype.Brute:
                    return instance.bruteEquips[0];
                case Archetype.Agressive:
                    return instance.agressiveEquips[0];
            }
        }

        return instance.supportersEquips[0];
    }

    public static RuntimeAnimatorController GetAnimator(Archetype archetype)
    {
        if (archetype > Archetype.MOffensive)
            return instance.animators[1];

        return instance.animators[0];
    }
}