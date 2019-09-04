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
    StandartToggle[] toggles;

    int index;

    void Awake()
    {
        characterPreview = GetComponentInChildren<CharacterPreview>();
        inputField = GetComponentInChildren<InputField>();
        toggles = GetComponentsInChildren<StandartToggle>();
    }

    public void Initialize(HeroFactory characterAttributes, int index)
    {
        this.index = index;

        UpdateValues(characterAttributes);

        PartyManager.OnEquipmentChange += EquipmentChange;

        characterPreview.CheckIfShouldChangeArchetype(characterAttributes.GetEquips());
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

    void UpdateValues(HeroFactory characterAttributes)
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
        }
    }

    void UpdateAttributeText(Equip[] equips)
    {
        var hp = 0;
        var atkp = 0;
        var atkm = 0;
        var defp = 0;
        var defm = 0;

        for (int i = 0; i < equips.Length; i++)
        {
            hp += equips[i].GetHp();
            atkp += equips[i].GetSubAttribute(Attributes.SubAttribute.ATKP);
            atkm += equips[i].GetSubAttribute(Attributes.SubAttribute.ATKM);
            defp += equips[i].GetSubAttribute(Attributes.SubAttribute.DEFP);
            defm += equips[i].GetSubAttribute(Attributes.SubAttribute.DEFM);
        }

        text.text = "HP: " + hp + "\nATKP: " + atkp + "\nATKM: " + atkm + "\nDEFP: " + defp + "\nDEFM: " + defm;
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
