using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class Test : MonoBehaviour
{
    InfiniteScroll infiniteScroll;
    [SerializeField] ToggleGroup charToggleGroup;
    //[SerializeField] StandartStats attributes1;
    [SerializeField] int selectedCharIndex = 0;
    int selectedEquipIndex = 0;
    Equip selectedEquip;

    public Button button;
    public bool flag = true;

    void Start()
    {
        var charToggles = charToggleGroup.GetComponentsInChildren<Toggle>();
        var partyLenght = PartyManager.GetParty().Length;
        for (int i = 0; i < charToggles.Length; i++)
        {
            //TODO colocar o nome também
            charToggles[i].interactable = i < partyLenght;
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

    //Equip GetSelectedEquip()
    //{
    //    return attributes1.GetEquips()[selectedEquipIndex];
    //}

    public void OnEquipToggleValueChange(int index)
    {
        if (selectedEquipIndex != index)
        {
            selectedEquipIndex = index;
            RefreshSelectedEquip();
            infiniteScroll.UpdateVisible();
        }
    }

    public void OnCharToggleValueChange(int index)
    {
        if (selectedCharIndex != index)
        {
            selectedCharIndex = index;
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
            //attributes1.GetEquips()[selectedEquipIndex] = equip;
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
