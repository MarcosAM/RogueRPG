using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static Equip GetEquip(int equipIndex) { return instace.equips[equipIndex]; }
    public static Equip[] GetAllEquips() { return instace.equips; }
}