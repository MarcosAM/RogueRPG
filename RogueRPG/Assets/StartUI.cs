using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public void GoToDungeon(Dungeon dungeon)
    {
        GameManager.getInstance().GoToDungeon(dungeon);
    }
}