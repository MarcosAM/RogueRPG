using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static GameManager instance = null;
    Dungeon currentDungeon;
    [SerializeField] Dungeon[] dungeons;
    List<Dungeon> clearedDungeons;

    void Awake()
    {
        MakeItASingleton();

        DontDestroyOnLoad(this);

        clearedDungeons = new List<Dungeon>();
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

    public int GetDungeonsCleared() { return clearedDungeons.Count; }

    public bool WasCleared(Dungeon dungeon) { return clearedDungeons.Contains(dungeon); }

    public void OnDungeonEnded()
    {
        clearedDungeons.Add(currentDungeon);

        if (currentDungeon.IsLast())
        {
            EndGame();
        }
        else
        {
            LoadScene(4);
        }
    }

    public void EndGame()
    {
        LoadScene(6);
    }
}