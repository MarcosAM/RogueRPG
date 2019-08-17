using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static GameManager instance = null;
    Dungeon currentDungeon;
    [SerializeField] Dungeon[] dungeons;
    int dungeonsCleared = 0;

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
    public Dungeon GetDungeon(int index) { return dungeons[index]; }

    public void UnlockDungeons()
    {
        dungeonsCleared++;

        foreach (Dungeon dungeon in dungeons)
        {
            if (dungeon.GetState() == Dungeon.State.Hidden && dungeon.GetPrerequisit() <= dungeonsCleared)
            {
                dungeon.SetState(Dungeon.State.JustDiscovered);
            }
        }

        currentDungeon.SetState(Dungeon.State.Beaten);
    }
}