using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class Test : MonoBehaviour
{
    InfiniteScroll infiniteScroll;
    //[SerializeField] ToggleGroup equipToggleGroup;
    [SerializeField] int selectedCharIndex = 0;
    int selectedEquipIndex = 0;
    Equip selectedEquip;
    //Toggle[] equipToggles;

    [SerializeField] CharacterListItem[] characterItens;
    [SerializeField] Text[] momentumEquipTxts;

    public Button button;
    public bool flag = true;

    void Start()
    {
        //equipToggles = equipToggleGroup.GetComponentsInChildren<Toggle>();

        var party = PartyManager.GetParty();
        //for (var i = 0; i < equipToggles.Length; i++)
        //{
        //    if (i < party.Length * 4)
        //    {
        //        equipToggles[i].interactable = true;
        //        UpdateEquipToggleLabel(i, party[i / 4].GetEquips()[i % 4].GetEquipName());
        //    }
        //    else
        //        equipToggles[i].interactable = false;
        //}

        for (var i = 0; i < characterItens.Length; i++)
        {
            if (i < party.Length)
                characterItens[i].Initialize(party[i], i);
            else
                characterItens[i].Initialize(null, i);
        }

        for (var i = 0; i < momentumEquipTxts.Length; i++)
        {
            if (i < party.Length)
            {
                momentumEquipTxts[i].text = Archetypes.GetMomentumEquipName(party[i].GetEquips());
            }
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
            //UpdateEquipToggleLabel(selectedEquipIndex + selectedCharIndex * 4, equip.GetEquipName());
            momentumEquipTxts[selectedCharIndex].text = Archetypes.GetMomentumEquipName(PartyManager.GetParty()[selectedCharIndex].GetEquips());
            infiniteScroll.UpdateVisible();
        }
    }

    void RefreshSelectedEquip()
    {
        selectedEquip = PartyManager.GetParty()[selectedCharIndex].GetEquips()[selectedEquipIndex];
    }

    //void UpdateEquipToggleLabel(int index, string equipName)
    //{
    //    equipToggles[index].GetComponentInChildren<Text>().text = equipName;
    //}

    private void OnDisable()
    {
        var equipListItems = FindObjectsOfType<EquipListItem>();
        foreach (var equipListItem in equipListItems)
        {
            equipListItem.OnBtnPress += OnEquipClicked;
        }
    }
}
