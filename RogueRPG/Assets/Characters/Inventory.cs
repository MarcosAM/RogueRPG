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
        foreach (bool b in availableEquips.Values)
        {
            Debug.Log(b);
        };
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

    public void SetEquipsAvailability(bool availability)
    {
        for (int i = 0; i < availableEquips.Count; i++)
        {
            availableEquips[i] = availability;
        }
    }

    public Dictionary<int, bool> GetAvailableEquips() { return availableEquips; }



    public Equip[] GetEquips() { return equips; }

    public void SetEquips(Character character, Equip[] equips)
    {
        this.side = character.Playable;

        this.equips = equips;

        InitiateAvailableEquips();
        Archetype = Archetypes.GetArchetype(this.equips);
        character.CreateEquipsSprites(this.equips);
        character.GetAttributes().UpdateAttributes(this.equips);
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
    }
}
