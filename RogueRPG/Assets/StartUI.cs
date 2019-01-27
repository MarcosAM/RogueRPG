using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{

    //public void StartMission(int index)
    //{
    //    GameManager gameManager = FindObjectOfType<GameManager>();
    //    //gameManager.ChangeSelectedQuest(index);
    //    //gameManager.LoadDungeonScene();
    //}

    public void GoToDungeon(Dungeon dungeon)
    {
        GameManager.getInstance().GoToDungeon(dungeon);
    }
}
