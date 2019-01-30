using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonBtn : MonoBehaviour
{
    Text dungeonName;
    Text dungeonLevel;
    Button button;
    [SerializeField]
    Dungeon dungeon;

    private void Awake()
    {
        gameObject.SetActive(false);
        Appear();
    }

    public void Appear()
    {
        gameObject.SetActive(true);

        var texts = GetComponentsInChildren<Text>();
        dungeonName = texts[0];
        dungeonLevel = texts[1];
        button = GetComponentInChildren<Button>();

        dungeonName.text = dungeon.GetDungeonName();
        dungeonLevel.text = "Level " + dungeon.GetLevel();
    }

    public void OnBtnClick()
    {
        GameManager.getInstance().GoToDungeon(dungeon);
    }
}