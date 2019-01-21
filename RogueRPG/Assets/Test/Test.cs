using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.InfiniteScroll;
using System.Linq;

public class Test : MonoBehaviour
{
    InfiniteScroll infiniteScroll;

    public Button button;
    public bool flag = true;

    void Start()
    {
        infiniteScroll = FindObjectOfType<InfiniteScroll>();
        infiniteScroll.OnFill += OnFillItem;
        infiniteScroll.OnHeight += OnHeightItem;

        button.onClick.AddListener(OnBtnPress);

        infiniteScroll.InitData(EquipDatabase.GetAllEquips().Length);
    }

    void OnFillItem(int index, GameObject item)
    {
        if (flag)
        {
            item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips()[index]);
            //item.GetComponentInChildren<Text>().text = EquipDatabase.GetAllEquips()[index].GetEquipName();
            //item.GetComponentInChildren<Image>().sprite = EquipDatabase.GetAllEquips()[index].GetFrontEquipPrefab().GetComponentInChildren<Image>().sprite;
        }
        else
        {
            item.GetComponent<EquipListItem>().Fill(EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index]);
            //item.GetComponentInChildren<Text>().text = EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index].GetEquipName();
            //item.GetComponentInChildren<Image>().sprite = EquipDatabase.GetAllEquips().Where(e => e.GetArchetype() == Archetypes.Archetype.Infantry).ToArray()[index].GetFrontEquipPrefab().GetComponentInChildren<Image>().sprite;
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
}
