using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class Test : MonoBehaviour
{
    InfiniteScroll infiniteScroll;
    ToggleGroup toggleGroup;
    [SerializeField] StandartStats attributes1;
    int selectedEquipIndex = 0;

    public Button button;
    public bool flag = true;

    void Start()
    {
        toggleGroup = FindObjectOfType<ToggleGroup>();

        infiniteScroll = FindObjectOfType<InfiniteScroll>();
        infiniteScroll.OnFill += OnFillItem;
        infiniteScroll.OnHeight += OnHeightItem;

        button.onClick.AddListener(OnBtnPress);

        infiniteScroll.InitData(EquipDatabase.GetAllEquips().Length);

        var equipListItems = FindObjectsOfType<EquipListItem>();
        print("Nós temos " + equipListItems.Length);
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
                item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips()[index], EquipDatabase.GetAllEquips()[index] == GetSelectedEquip());
            else
                item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips()[index].GetArchetype());
        }
        else
        {
            if (EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index].GetAmount() > 0)
                item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index], EquipDatabase.GetAllEquips()[index] == GetSelectedEquip());
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

    Equip GetSelectedEquip()
    {
        return attributes1.GetEquips()[selectedEquipIndex];
    }

    public void OnToggleValueChange(int index)
    {
        if (selectedEquipIndex != index)
        {
            selectedEquipIndex = index;
            infiniteScroll.UpdateVisible();
        }
    }

    public void OnEquipClicked(Equip equip)
    {
        if (equip.GetHowManyLeft() > 0)
        {
            attributes1.GetEquips()[selectedEquipIndex] = equip;
            infiniteScroll.UpdateVisible();
        }
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
