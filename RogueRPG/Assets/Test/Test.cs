using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class Test : MonoBehaviour
{
    InfiniteScroll infiniteScroll;
    [SerializeField] ToggleGroup equipToggleGroup;
    [SerializeField] int selectedCharIndex = 0;
    int selectedEquipIndex = 0;
    Equip selectedEquip;

    public Button button;
    public bool flag = true;

    void Start()
    {
        var equipToggles = equipToggleGroup.GetComponentsInChildren<Toggle>();
        var partyLenght = PartyManager.GetParty().Length;
        for (int i = 0; i < equipToggles.Length; i++)
        {
            //TODO colocar o nome também
            equipToggles[i].interactable = i < partyLenght * 4;
        }

        RefreshSelectedEquip();

        infiniteScroll = FindObjectOfType<InfiniteScroll>();
        infiniteScroll.OnFill += OnFillItem;
        infiniteScroll.OnHeight += OnHeightItem;

        button.onClick.AddListener(OnBtnPress);

        infiniteScroll.InitData(EquipDatabase.GetAllEquips().Length);

        var equipListItems = FindObjectsOfType<EquipListItem>();
        foreach (var equipListItem in equipListItems)
        {
            equipListItem.OnBtnPress += OnEquipClicked;
        }
    }

    void OnFillItem(int index, GameObject item)
    {
        if (flag)
        {
            if (EquipDatabase.GetAllEquips()[index].GetAmount() > 0)
                item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips()[index], EquipDatabase.GetAllEquips()[index] == selectedEquip);
            else
                item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips()[index].GetArchetype());
        }
        else
        {
            if (EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index].GetAmount() > 0)
                item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index], EquipDatabase.GetAllEquips()[index] == selectedEquip);
            else
                item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index].GetArchetype());
        }
    }

    int OnHeightItem(int index)
    {
        return 40;
    }

    void OnBtnPress()
    {
        flag = flag ? false : true;
        if (flag)
        {
            infiniteScroll.InitData(EquipDatabase.GetAllEquips().Length);
        }
        else
        {
            infiniteScroll.InitData(EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray().Length);
        }
    }

    public void OnEquipToggleValueChange(int index)
    {
        if (selectedEquipIndex != index % 4 || selectedCharIndex != index / 4)
        {
            selectedEquipIndex = index % 4;
            selectedCharIndex = index / 4;

            RefreshSelectedEquip();
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
