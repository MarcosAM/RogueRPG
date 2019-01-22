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
    Toggle[] equipToggles;

    [SerializeField] Text[] attributesTxts;
    [SerializeField] InputField[] inputName;

    public Button button;
    public bool flag = true;

    void Start()
    {
        equipToggles = equipToggleGroup.GetComponentsInChildren<Toggle>();
        //var partyLenght = PartyManager.GetParty().Length;

        var party = PartyManager.GetParty();
        for (var i = 0; i < equipToggles.Length; i++)
        {
            if (i < party.Length * 4)
            {
                equipToggles[i].interactable = true;
                UpdateEquipToggleLabel(i, party[i / 4].GetEquips()[i % 4].GetEquipName());
            }
            else
                equipToggles[i].interactable = false;
        }

        for (var i = 0; i < attributesTxts.Length; i++)
        {
            if (i < party.Length)
                UpdateAttributeText(i, party[i].GetEquips());
            else
                UpdateAttributeText(i, null);
        }

        for (var i = 0; i < inputName.Length; i++)
        {
            if (i < party.Length)
            {
                inputName[i].interactable = true;

                inputName[i].text = party[i].GetName();
            }
            else
            {
                inputName[i].interactable = false;
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
            infiniteScroll.UpdateVisible();
        }
    }

    public void OnEquipClicked(Equip equip)
    {
        if (equip.GetHowManyLeft() > 0)
        {
            PartyManager.EquipPartyMemberWith(selectedCharIndex, selectedEquipIndex, equip);
            RefreshSelectedEquip();
            UpdateEquipToggleLabel(selectedEquipIndex + selectedCharIndex * 4, equip.GetEquipName());
            UpdateAttributeText(selectedCharIndex, PartyManager.GetParty()[selectedCharIndex].GetEquips());
            infiniteScroll.UpdateVisible();
        }
    }

    void RefreshSelectedEquip()
    {
        selectedEquip = PartyManager.GetParty()[selectedCharIndex].GetEquips()[selectedEquipIndex];
    }

    void UpdateAttributeText(int index, Equip[] equips)
    {
        if (equips != null)
        {
            var hp = 1;
            var atk = 1;
            var atkm = 1;
            var def = 1;
            var defm = 1;

            foreach (var equip in equips)
            {
                hp += equip.GetHp();
                atk += equip.GetAtk();
                atkm += equip.GetAtkm();
                def += equip.GetDef();
                defm += equip.GetDefm();
            }

            attributesTxts[index].text = "HP: " + hp + "\nATK: " + atk + "\nATKM: " + atkm + "\nDEF: " + def + "\nDEFM: " + defm;
        }
        else
            attributesTxts[index].text = "";
    }

    void UpdateEquipToggleLabel(int index, string equipName)
    {
        equipToggles[index].GetComponentInChildren<Text>().text = equipName;
    }

    public void SetName(int index)
    {
        PartyManager.GetParty()[index].SetName(inputName[index].text);
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
