using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Equip[] equips;
    bool[] availableEquips;
    int level = 0;
    public Archetypes.Archetype Archetype { get; set; }

    public void CheckIfEquipsShouldBeRefreshed()
    {
        if (AtLeastOneEquipAvailable())
        {
            return;
        }
        else
        {
            SetEquipsAvailability(true);
        }
    }



    public bool IsEquipAvailable(int index)
    {
        if (index == availableEquips.Length - 1)
        {
            return FindObjectOfType<Momentum>().IsMomentumFull();
        }
        return availableEquips[index];
    }

    public bool AtLeastOneEquipAvailable()
    {
        foreach (bool b in availableEquips)
        {
            if (b)
                return true;
        }
        return false;
    }

    public bool IsMomentumEquip(Equip equip)
    {
        return equip == equips[equips.Length - 1];
    }

    public void SetEquipsAvailability(bool availability)
    {
        for (int i = 0; i < availableEquips.Length; i++)
        {
            availableEquips[i] = availability;
        }
        availableEquips[availableEquips.Length - 1] = false;
    }

    public bool[] GetAvailableEquips() { return availableEquips; }



    public Equip[] GetEquips() { return equips; }

    public void SetEquips(Character character, Equip[] equips)
    {
        this.equips = equips;
        InitiateAvailableEquips();
        InitiateCharacterAttributes(character);
        Archetype = Archetypes.GetArchetype(this.equips);
        InitiateLevel();
        equips[equips.Length - 1] = Archetypes.GetMomentumEquip(Archetype, level);
    }



    public List<Equip> GetUsableEquips()
    {
        List<Equip> usableSkills = new List<Equip>();
        for (int i = 0; i < equips.Length; i++)
        {
            if (IsEquipAvailable(i))
            {
                usableSkills.Add(equips[i]);
            }
        }
        return usableSkills;
    }



    void InitiateAvailableEquips()
    {
        availableEquips = new bool[equips.Length];
        for (int i = 0; i < availableEquips.Length; i++)
        {
            availableEquips[i] = true;
        }
        availableEquips[availableEquips.Length - 1] = false;
    }

    void InitiateCharacterAttributes(Character character)
    {
        foreach (var equip in equips)
        {
            character.GetStat(Stat.Stats.Atk).AddToStatBase(equip.GetAtk());
            character.GetStat(Stat.Stats.Atkm).AddToStatBase(equip.GetAtkm());
            character.GetStat(Stat.Stats.Def).AddToStatBase(equip.GetDef());
            character.GetStat(Stat.Stats.Defm).AddToStatBase(equip.GetDefm());

            character.AddToMaxHp(equip.GetHp());
        }
    }

    void InitiateLevel()
    {
        foreach (var equip in equips)
        {
            level += equip.GetLevel();
        }
    }
}
