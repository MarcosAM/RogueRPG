using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EquipDatabase : MonoBehaviour
{
    [SerializeField] Equip[] equips;

    static EquipDatabase instace;

    private void Awake()
    {
        instace = this;
        DontDestroyOnLoad(gameObject);

        foreach (var equip in equips)
        {
            equip.SetHowManyLeft(equip.GetAmount());
        }

        foreach (var attribute in PartyManager.GetParty())
        {
            foreach (var equip in attribute.GetEquips())
            {
                equip.SetHowManyLeft(equip.GetHowManyLeft() - 1);
            }
        }
    }

    public static Equip UnlockNewEquip(Archetypes.Archetype archetype1, Archetypes.Archetype archetype2, int level1, int level2, int dungeonLevel)
    {
        int lowerLevel = level1 >= level2 ? level2 : level1;
        int sumLevel = level1 + level2;
        if (sumLevel > dungeonLevel)
            sumLevel = dungeonLevel;

        var possibleEquips = instace.equips.Where(e => (e.GetLevel() >= lowerLevel && e.GetLevel() <= sumLevel) && (e.GetArchetype() == archetype1 || e.GetArchetype() == archetype2)).ToArray();

        if (possibleEquips.Length <= 0)
            possibleEquips = instace.equips;

        return possibleEquips[Random.Range(0, possibleEquips.Length - 1)];
    }

    public static Equip GetEquip(int equipIndex) { return instace.equips[equipIndex]; }
    public static Equip[] GetAllEquips() { return instace.equips; }
}