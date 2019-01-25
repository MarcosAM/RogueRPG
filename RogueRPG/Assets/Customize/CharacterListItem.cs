﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListItem : MonoBehaviour
{
    CharacterPreview characterPreview;
    InputField inputField;
    [SerializeField]
    Text text;
    StandartToggle[] toggles;
    [SerializeField] Text momentumEquipTxt;

    int index;

    void Awake()
    {
        characterPreview = GetComponentInChildren<CharacterPreview>();
        inputField = GetComponentInChildren<InputField>();
        toggles = GetComponentsInChildren<StandartToggle>();
    }

    public void Initialize(StandartStats characterAttributes, int index)
    {
        this.index = index;

        UpdateValues(characterAttributes);

        PartyManager.OnEquipmentChange += EquipmentChange;
    }

    void EquipmentChange(int index)
    {
        if (this.index == index)
        {
            UpdateValues(PartyManager.GetParty()[index]);

            for (var i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].getToggle().isOn)
                {
                    var equips = PartyManager.GetParty()[index].GetEquips();

                    characterPreview.ChangeEquipObject(equips[i]);
                    characterPreview.CheckIfShouldChangeArchetype(equips);
                }
            }
        }
    }

    void UpdateValues(StandartStats characterAttributes)
    {
        if (characterAttributes)
        {
            characterPreview.gameObject.SetActive(true);
            characterPreview.CheckIfShouldChangeArchetype(characterAttributes.GetEquips());

            inputField.interactable = true;
            inputField.text = characterAttributes.GetName();

            UpdateAttributeText(characterAttributes.GetEquips());

            for (var i = 0; i < toggles.Length; i++)
            {
                toggles[i].SetInterectable(true);
                toggles[i].getText().text = characterAttributes.GetEquips()[i].GetEquipName();
            }

            momentumEquipTxt.text = Archetypes.GetMomentumEquipName(characterAttributes.GetEquips());
        }
        else
        {
            characterPreview.gameObject.SetActive(false);
            inputField.interactable = false;
            text.text = "";

            for (var i = 0; i < toggles.Length; i++)
            {
                toggles[i].SetInterectable(false);
            }

            momentumEquipTxt.text = "Momentum Equip";
        }
    }

    void UpdateAttributeText(Equip[] equips)
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

        text.text = "HP: " + hp + "\nATK: " + atk + "\nATKM: " + atkm + "\nDEF: " + def + "\nDEFM: " + defm;
    }

    public void SetName(string name)
    {
        PartyManager.GetParty()[index].SetName(name);
    }

    public CharacterPreview GetCharacterPreview() { return characterPreview; }

    private void OnDisable()
    {
        PartyManager.OnEquipmentChange -= EquipmentChange;
    }
}
