using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] StandartStats[] standartStats;

    static PartyManager instance;

    void Awake()
    {
        instance = this;
    }

    public static StandartStats[] GetParty() { return instance.standartStats; }

    public static void EquipPartyMemberWith(int charIndex, int equipIndex, Equip equip)
    {
        equip.SetHowManyLeft(equip.GetHowManyLeft() - 1);
        instance.standartStats[charIndex].GetEquips()[equipIndex].SetHowManyLeft(equip.GetHowManyLeft() + 1);

        instance.standartStats[charIndex].GetEquips()[equipIndex] = equip;
    }
}