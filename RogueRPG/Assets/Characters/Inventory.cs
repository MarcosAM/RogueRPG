using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    Equip[] equips;
    Dictionary<int, bool> availableEquips;
    int level = 0;
    public Archetypes.Archetype Archetype { get; set; }
    bool side;

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
        if (index == availableEquips.Count - 1)
        {
            return FindObjectOfType<Momentum>().IsMomentumFull(side);
        }
        return availableEquips[index];
    }

    public bool AtLeastOneEquipAvailable()
    {
        for (int i = 0; i < availableEquips.Count; i++)
        {
            if (availableEquips[i])
                return true;
        }
        return false;
    }

    public bool IsMomentumEquip(int equipIndex)
    {
        return equipIndex == equips.Length - 1;
    }

    public void SetEquipsAvailability(bool availability)
    {
        for (int i = 0; i < availableEquips.Count; i++)
        {
            availableEquips[i] = availability;
        }
        availableEquips[availableEquips.Count - 1] = false;
    }

    public Dictionary<int, bool> GetAvailableEquips() { return availableEquips; }



    public Equip[] GetEquips() { return equips; }

    public void SetEquips(Character character, Equip[] equips)
    {
        this.side = character.IsPlayable();
        this.equips = equips;
        InitiateAvailableEquips();
        Archetype = Archetypes.GetArchetype(this.equips);
        InitiateLevel();
        equips[equips.Length - 1] = Archetypes.GetMomentumEquip(Archetype, level);
        character.GetAttributes().UpdateAttributes(equips);
    }



    public List<int> GetUsableEquips()
    {
        return availableEquips.Where(e => e.Value).Select(e => e.Key).ToList();
    }



    void InitiateAvailableEquips()
    {
        availableEquips = new Dictionary<int, bool>();
        for (int i = 0; i < equips.Length; i++)
        {
            availableEquips[i] = true;
        }
        availableEquips[equips.Length - 1] = false;
    }
    void InitiateLevel()
    {
        foreach (var equip in equips)
        {
            if (equip)
                level += equip.GetLevel();
        }
    }
}
