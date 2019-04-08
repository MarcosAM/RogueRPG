﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PartyManager : MonoBehaviour
{
    [SerializeField] HeroFactory[] standartStats;

    static PartyManager instance;

    public static event Action<int> OnEquipmentChange;



    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public static HeroFactory[] GetParty() { return GetInstance().standartStats; }

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