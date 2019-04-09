﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class EquipList : MonoBehaviour
{
    InfiniteScroll infiniteScroll;
    [SerializeField] int selectedCharIndex = 0;
    int selectedEquipIndex = 0;
    Equip selectedEquip;

    [SerializeField] CharacterListItem[] characterItens;

    void Start()
    {
        var party = PartyManager.GetParty();

        for (var i = 0; i < characterItens.Length; i++)
        {
            if (i < party.Length)
                characterItens[i].Initialize(party[i], i);
            else
                characterItens[i].Initialize(null, i);
        }

        RefreshSelectedEquip();

        infiniteScroll = FindObjectOfType<InfiniteScroll>();
        infiniteScroll.OnFill += OnFillItem;
        infiniteScroll.OnHeight += OnHeightItem;

        //TODO fazer ele pegar os equips e salvar na própria classe para não precisar ficar chamando mais o tempo todo
        infiniteScroll.InitData(LootManager.GetAllPlayersEquips().Length);

        var equipListItems = FindObjectsOfType<EquipListItem>();
        foreach (var equipListItem in equipListItems)
        {
            equipListItem.OnBtnPress += OnEquipClicked;
        }
    }

    void OnFillItem(int index, GameObject item)
    {
        //if (EquipDatabase.GetAllEquips()[index].GetAmount() > 0)
        item.GetComponent<EquipListItem>().Fill(LootManager.GetAllPlayersEquips()[index], LootManager.GetAllPlayersEquips()[index] == selectedEquip);
        //else
        //item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips()[index].GetArchetype());
    }

    int OnHeightItem(int index)
    {
        return 40;
    }

    public void OnEquipToggleValueChange(int index)
    {
        if (selectedEquipIndex != index % 4 || selectedCharIndex != index / 4)
        {
            selectedEquipIndex = index % 4;
            selectedCharIndex = index / 4;

            RefreshSelectedEquip();
            characterItens[selectedCharIndex].GetCharacterPreview().SwitchEquip(selectedEquip);
            infiniteScroll.UpdateVisible();
        }
    }

    public void OnEquipClicked(Equip equip)
    {
        if (equip.GetHowManyLeft() > 0)
        {
            PartyManager.EquipPartyMemberWith(selectedCharIndex, selectedEquipIndex, equip);
            RefreshSelectedEquip();
            infiniteScroll.UpdateVisible();
        }
    }

    void RefreshSelectedEquip()
    {
        selectedEquip = PartyManager.GetParty()[selectedCharIndex].GetEquips()[selectedEquipIndex];
    }

    private void OnDisable()
    {
        var equipListItems = FindObjectsOfType<EquipListItem>();
        foreach (var equipListItem in equipListItems)
        {
            equipListItem.OnBtnPress += OnEquipClicked;
        }
    }
}
