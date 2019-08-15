using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class EquipList : MonoBehaviour
{
    //InfiniteScroll infiniteScroll;
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

        RectTransform srContent = scrollRect.content;


        srContent.sizeDelta = new Vector2(srContent.sizeDelta.x, allPlayersEquips.Length * 40);

        for (var i = 0; i < allPlayersEquips.Length; i++)
        {
            EquipListItem listItem = Instantiate(equipListItem);
            listItem.transform.SetParent(srContent, false);
            listItem.Fill(allPlayersEquips[i], allPlayersEquips[i] == selectedEquip);
            listItem.OnBtnPress += OnEquipClicked;


            var y = (srContent.rect.height / 2) - (listItem.GetComponent<RectTransform>().rect.height / 2);

            Vector3 position = listItem.GetComponent<RectTransform>().localPosition;

            listItem.transform.localPosition = new Vector3(position.x, -(listItem.GetComponent<RectTransform>().rect.height * (i + 1) - listItem.GetComponent<RectTransform>().rect.height / 2), position.z);
        }



        //infiniteScroll = FindObjectOfType<InfiniteScroll>();
        //infiniteScroll.OnFill += OnFillItem;
        //infiniteScroll.OnHeight += OnHeightItem;

        //infiniteScroll.InitData(allPlayersEquips.Length);

        /*
        var equipListItems = FindObjectsOfType<EquipListItem>();
        foreach (var equipListItem in equipListItems)
        {
            equipListItem.OnBtnPress += OnEquipClicked;
        }
        */
    }

    void OnFillItem(int index, GameObject item)
    {
        //item.GetComponent<EquipListItem>().Fill(allPlayersEquips[index], allPlayersEquips[index] == selectedEquip, index);
        item.GetComponent<EquipListItem>().Fill(allPlayersEquips[index], allPlayersEquips[index] == selectedEquip);
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
            //infiniteScroll.UpdateVisible();
        }
    }

    public void OnEquipClicked(Equip equip)
    {
        if (equip.GetHowManyLeft() > 0)
        {
            Debug.Log("Ei!");
            PartyManager.EquipPartyMemberWith(selectedCharIndex, selectedEquipIndex, equip);
            RefreshSelectedEquip();
            //infiniteScroll.UpdateVisible();
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
