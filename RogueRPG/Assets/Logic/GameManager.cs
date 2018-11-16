using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static GameManager instance = null;
    [SerializeField] Quest selectedQuest;
    List<Character> playerCharacters = new List<Character>();
    [SerializeField] GameObject characterPrefab;
    [SerializeField] StandartStats pcGuerreiroStats;
    [SerializeField] StandartStats pcMagoStats;
    [SerializeField] List<StandartStats> pcStats;
    [SerializeField] List<string> pcNames;

    void Awake()
    {
        MakeItASingleton();

        DontDestroyOnLoad(this);
    }

    //TODO colocar outra coisa para se responsabilizar por mudar de cenas
    public void LoadDungeonScene()
    {
        SceneManager.LoadScene(BATTLE_SCENE_INDEX);
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

    public List<Character> getEnemiesAtFloor(int floor)
    {
        List<Character> characters = new List<Character>();
        foreach (StandartStats stats in selectedQuest.getCurrentDungeon().getBattleGroups()[floor].GetEnemiesStats())
        {
            if (stats == null)
            {
                characters.Add(null);
            }
            else
            {
                characters.Add(CreateCharacter(false, stats));
            }
        }
        return characters;
    }
    public Battleground.BattlegroundSize GetBattlegroundSize(int floor)
    {
        return selectedQuest.getCurrentDungeon().getBattleGroups()[floor].battlegroundSize;
    }
    public List<StandartStats> getHeroesStats() { return pcStats; }
    public List<string> getHeroesNames() { return pcNames; }
    public Quest getSelectedQuest() { return selectedQuest; }
    public static GameManager getInstance() { return instance; }
    public List<Character> getPlayerCharacters() { return playerCharacters; }

    public const int BATTLE_SCENE_INDEX = 1;

    public Character CreateCharacter(bool alignment, StandartStats standartStats)
    {
        GameObject gO = Instantiate(characterPrefab);
        Character character;
        if (alignment)
        {
            character = gO.gameObject.AddComponent(typeof(PlayableCharacter)) as PlayableCharacter;
        }
        else
        {
            character = gO.gameObject.AddComponent(typeof(NonPlayableCharacter)) as NonPlayableCharacter;
        }
        character.setStats(standartStats);
        return character;
    }
}