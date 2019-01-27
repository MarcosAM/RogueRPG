using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static GameManager instance = null;
    Dungeon currentDungeon;

    void Awake()
    {
        MakeItASingleton();

        DontDestroyOnLoad(this);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void MakeItASingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager getInstance() { return instance; }

    public const int BATTLE_SCENE_INDEX = 1;

    public void GoToDungeon(Dungeon dungeon)
    {
        currentDungeon = dungeon;

        SceneManager.LoadScene(BATTLE_SCENE_INDEX);
    }

    public Dungeon GetCurrentDungeon() { return currentDungeon; }
}