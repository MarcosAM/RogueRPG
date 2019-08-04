using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archetypes : MonoBehaviour
{
    //TODO me lembrar de depois no botão start zerar tudo ao estado normal de jogo
    [SerializeField] protected Equip[] warlockEquips;
    [SerializeField] protected Equip[] wizardEquips;
    [SerializeField] protected Equip[] priestEquips;
    [SerializeField] protected Equip[] rangerEquips;
    [SerializeField] protected Equip[] thiefEquips;
    [SerializeField] protected Equip[] fighterEquips;
    [SerializeField] protected Equip[] knightEquips;
    [SerializeField] protected Equip[] berserkerEquips;
    [SerializeField] protected RectTransform[] hats;
    [SerializeField] Sprite archetypesIcon;

    [SerializeField] RuntimeAnimatorController[] animators;
    static Archetypes instance;

    private void Awake()
    {
        instance = this;
    }

    public enum Archetype { Warlock, Wizard, Priest, Ranger, Thief, Fighter, Knight, Berserker }

    public static Archetype GetArchetype(Equip[] equips)
    {
        Dictionary<Archetype, int> archetypesAmount = new Dictionary<Archetype, int>();

        foreach (Equip equip in equips)
        {
            if (!archetypesAmount.ContainsKey(equip.GetArchetype()))
            {
                archetypesAmount.Add(equip.GetArchetype(), 1);
                break;
            }

            archetypesAmount[equip.GetArchetype()] ++;
        }

        var archetype = Archetype.Warlock;
        var amount = 0;

        foreach (var item in archetypesAmount)
        {
            if (item.Value > amount)
            {
                archetype = item.Key;
                amount = item.Value;
            }
        }
        return archetype;
    }

    public static string GetMomentumEquipName(Equip[] equips)
    {
        Dictionary<Archetype, int> archetypesAmount = new Dictionary<Archetype, int>();

        foreach (Equip equip in equips)
        {
            if (!archetypesAmount.ContainsKey(equip.GetArchetype()))
            {
                archetypesAmount.Add(equip.GetArchetype(), 1);
                break;
            }

            archetypesAmount[equip.GetArchetype()] ++;
        }

        var archetype = Archetype.Warlock;
        var amount = 0;

        foreach (var item in archetypesAmount)
        {
            if (item.Value > amount)
            {
                archetype = item.Key;
                amount = item.Value;
            }
        }
        return GetMomentumEquip(archetype, amount).GetEquipName();
    }

    public static Equip GetMomentumEquip(Archetype archetype, int level)
    {
        try
        {
            switch (archetype)
            {
                case Archetype.Warlock:
                    return instance.warlockEquips[level];
                case Archetype.Wizard:
                    return instance.wizardEquips[level];
                case Archetype.Priest:
                    return instance.priestEquips[level];
                case Archetype.Ranger:
                    return instance.rangerEquips[level];
                case Archetype.Thief:
                    return instance.thiefEquips[level];
                case Archetype.Fighter:
                    return instance.fighterEquips[level];
                case Archetype.Knight:
                    return instance.knightEquips[level];
                case Archetype.Berserker:
                    return instance.berserkerEquips[level];
            }
        }
        catch
        {
            switch (archetype)
            {
                case Archetype.Warlock:
                    return instance.warlockEquips[0];
                case Archetype.Wizard:
                    return instance.wizardEquips[0];
                case Archetype.Priest:
                    return instance.priestEquips[0];
                case Archetype.Ranger:
                    return instance.rangerEquips[0];
                case Archetype.Thief:
                    return instance.thiefEquips[0];
                case Archetype.Fighter:
                    return instance.fighterEquips[0];
                case Archetype.Knight:
                    return instance.knightEquips[0];
                case Archetype.Berserker:
                    return instance.berserkerEquips[0];
            }
        }

        return instance.warlockEquips[0];
    }

    public static RectTransform GetHat(Archetype archetype) { return Instantiate(instance.hats[(int)archetype]); }

    public static RuntimeAnimatorController GetAnimator(Archetype archetype)
    {
        if (archetype > Archetype.Priest)
            return instance.animators[1];

        return instance.animators[0];
    }

    public static Sprite GetArchetypeIcon(Archetype archetype)
    {
        return instance.archetypesIcon;
    }
}