using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListItem : MonoBehaviour
{
    CharacterPreview characterPreview;
    InputField inputField;
    [SerializeField]
    Text text;
    Toggle[] toggles;

    int index;

    void Awake()
    {
        characterPreview = GetComponentInChildren<CharacterPreview>();
        inputField = GetComponentInChildren<InputField>();
        toggles = GetComponentsInChildren<Toggle>();
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
                if (toggles[i].isOn)
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
                toggles[i].interactable = true;
                toggles[i].GetComponentInChildren<Text>().text = characterAttributes.GetEquips()[i].GetEquipName();
            }
        }
        else
        {
            characterPreview.gameObject.SetActive(false);
            inputField.interactable = false;
            text.text = "";

            for (var i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = false;
            }
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
