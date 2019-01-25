using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PartyManager : MonoBehaviour
{
    [SerializeField] StandartStats[] standartStats;

    static PartyManager instance;

    public static event Action<int> OnEquipmentChange;



    void Awake()
    {
        instance = this;
    }

    public static StandartStats[] GetParty() { return GetInstance().standartStats; }

    public static void EquipPartyMemberWith(int charIndex, int equipIndex, Equip equip)
    {
        equip.SetHowManyLeft(equip.GetHowManyLeft() - 1);
        GetInstance().standartStats[charIndex].GetEquips()[equipIndex].SetHowManyLeft(equip.GetHowManyLeft() + 1);

        GetInstance().standartStats[charIndex].GetEquips()[equipIndex] = equip;

        EquipmentChange(charIndex);
    }

    static void EquipmentChange(int index)
    {
        if (OnEquipmentChange != null)
        {
            OnEquipmentChange(index);
        }
    }

    static PartyManager GetInstance()
    {
        if (!instance)
            instance = FindObjectOfType<PartyManager>();

        return instance;
    }
}