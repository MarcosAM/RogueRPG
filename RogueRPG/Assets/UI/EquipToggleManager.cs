using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipToggleManager : MonoBehaviour
{

    private TurnManager combatBehavior;
    [SerializeField] private List<EquipToggle> equipToggles = new List<EquipToggle>();
    private PlayerInputManager playerInputManager;
    private ToggleGroup toggleGroup;

    void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public bool AnyToggleOne() { return toggleGroup.AnyTogglesOn(); }

    public void OnAnyToggleChange(EquipToggle equipToggle)
    {
        if (equipToggle.getToggle().isOn)
        {
            playerInputManager.ReportNewSelectedEquipToggle(equipToggle);
            return;
        }
        playerInputManager.ReportNewSelectedEquipToggle(null);
    }

    public void SetAllEquipTogglesOff()
    {
        toggleGroup.SetAllTogglesOff();
    }

    public void ShowEquipTogglesFor(Inventory inventory)
    {
        gameObject.SetActive(true);

        int i;
        for (i = 0; i < inventory.GetEquips().Length - 1; i++)
        {
            equipToggles[i].gameObject.SetActive(true);
            equipToggles[i].getText().text = inventory.GetEquips()[i].GetEquipName();
            equipToggles[i].SetInterectable(inventory.IsEquipAvailable(i));
        }

        equipToggles[equipToggles.Count - 1].gameObject.SetActive(true);
        equipToggles[equipToggles.Count - 1].getText().text = inventory.GetEquips()[i].GetEquipName();
        equipToggles[equipToggles.Count - 1].SetInterectable(inventory.IsEquipAvailable(i));

        for (var j = i; j < equipToggles.Count - 1; j++)
        {
            print("Cheguei aqui e não deveria!");
            equipToggles[j].gameObject.SetActive(false);
        }
    }

    public int GetSelectedEquipIndex()
    {
        for (int i = 0; i < equipToggles.Count; i++)
        {
            if (equipToggles[i].getToggle().isOn)
            {
                return i;
            }
        }
        return 1;
    }

    public void HideEquipToggles()
    {
        gameObject.SetActive(false);
    }
}