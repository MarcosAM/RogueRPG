using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LootOptions : MonoBehaviour
{
    ToggleGroup toggleGroup;
    List<LootOption> lootOptions = new List<LootOption>();

    void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        lootOptions = GetComponentsInChildren<LootOption>().ToList();
    }

    public void RefreshOptions(Equip optionA, Equip optionB)
    {
        lootOptions[0].Refresh(optionA);
        lootOptions[1].Refresh(optionB);
    }
}