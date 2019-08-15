using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class EquipList : MonoBehaviour
{
    ScrollRect scrollRect;
    [SerializeField] int selectedCharIndex = 0;
    int selectedEquipIndex = 0;
    Equip selectedEquip;

    [SerializeField] CharacterListItem[] characterItens;
    [SerializeField] EquipListItem equipListItem;

    Equip[] allPlayersEquips;

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

        allPlayersEquips = LootManager.GetAllPlayersEquips();

        scrollRect = FindObjectOfType<ScrollRect>();

        RefreshListItems();
    }

    void UpdateScrollHeight(RectTransform rect)
    {
        int contentHeight = allPlayersEquips.Length * 40;

        if (rect.sizeDelta.y != contentHeight)
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, contentHeight);
    }

    void InstatiateListItem(RectTransform rect, Equip equip, int index)
    {
        EquipListItem listItem = Instantiate(equipListItem);
        listItem.transform.SetParent(rect, false);
        listItem.Fill(equip, equip == selectedEquip, PartyManager.GetParty().Any(hero => hero.GetEquips().Contains(equip)));
        listItem.OnBtnPress += OnEquipClicked;


        var y = (rect.rect.height / 2) - (listItem.GetComponent<RectTransform>().rect.height / 2);

        Vector3 position = listItem.GetComponent<RectTransform>().localPosition;

        listItem.transform.localPosition = new Vector3(position.x, -(listItem.GetComponent<RectTransform>().rect.height * (index + 1) - listItem.GetComponent<RectTransform>().rect.height / 2), position.z);
    }

    void UpdateListItem(RectTransform rect, int index, Equip equip)
    {
        EquipListItem[] listItems = rect.GetComponentsInChildren<EquipListItem>();
        listItems[index].Fill(equip, equip == selectedEquip, PartyManager.GetParty().Any(hero => hero.GetEquips().Contains(equip)));
    }

    void RefreshListItems()
    {
        RectTransform srContent = scrollRect.content;

        UpdateScrollHeight(srContent);

        for (var i = 0; i < allPlayersEquips.Length; i++)
        {
            if (i < srContent.childCount)
            {
                UpdateListItem(srContent, i, allPlayersEquips[i]);
            }
            else
            {
                InstatiateListItem(srContent, allPlayersEquips[i], i);
            }
        }
    }

    public void OnEquipClicked(Equip equip)
    {
        PartyManager.EquipPartyMemberWith(selectedCharIndex, selectedEquipIndex, equip);
        RefreshSelectedEquip();
        RefreshListItems();
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
