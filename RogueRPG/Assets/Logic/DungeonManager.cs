using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class DungeonManager : MonoBehaviour
{

    static DungeonManager instance = null;
    [SerializeField] List<Character> initiativeOrder = new List<Character>();
    int round;
    int dungeonFloor = 0;
    Battleground battleground;

    void Start()
    {
        battleground = FindObjectOfType<Battleground>();
        GameManager gameManager = GameManager.getInstance();
        MakeItASingleton();

        List<Character> pcs = new List<Character>();
        for (int i = 0; i < gameManager.getHeroesStats().Count; i++)
        {
            Character character = gameManager.CreateCharacter(true, gameManager.getHeroesStats()[i]);
            character.setName(gameManager.getHeroesNames()[i]);
            pcs.Add(character);
        }

        battleground.SetAvailableSide(pcs);
        battleground.SetAvailableSide(gameManager.getEnemiesAtFloor(dungeonFloor));

        AddToInitiative(battleground.GetAliveCharactersFrom(true));
        AddToInitiative(battleground.GetAliveCharactersFrom(false));

        foreach (Character character in initiativeOrder)
        {
            character.refresh();
        }

        battleground.Size = gameManager.GetBattlegroundSize(dungeonFloor);

        round = 0;
        TryToStartTurn();
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

    void TryToStartTurn()
    {
        initiativeOrder[0].getBehavior().StartTurn();
    }

    void NextTurn()
    {
        if (DidOnePartyLost() > 0)
        {
            EndBattleAndCheckIfDungeonEnded();
            return;
        }
        if (DidOnePartyLost() == 0)
        {
            if (initiativeOrder.Count > 0)
            {
                AdvanceInitiative(initiativeOrder);
                round++;
                TryToStartTurn();
                return;
            }
        }
        if (DidOnePartyLost() < 0)
        {
            SceneManager.LoadScene(3);
            return;
        }
    }

    public void AdvanceInitiative(List<Character> initiative)
    {
        initiative.Add(initiative[0]);
        initiative.RemoveAt(0);
    }

    void DeleteFromInitiative(Character combatant)
    {
        initiativeOrder.Remove(combatant);
    }

    public void AddToInitiative(Character character)
    {
        if (character != null)
            initiativeOrder.Add(character);
    }

    void AddToInitiative(List<Character> characters)
    {
        foreach (Character hero in characters)
        {
            if (hero != null)
                initiativeOrder.Add(hero);
        }
    }

    void EndBattleAndCheckIfDungeonEnded()
    {
        dungeonFloor++;
        GameManager gameManager = GameManager.getInstance();
        if (dungeonFloor < gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups().Count)
        {
            AdvanceInitiative(initiativeOrder);
            battleground.SetAvailableSide(gameManager.getEnemiesAtFloor(dungeonFloor));
            battleground.Size = gameManager.GetBattlegroundSize(dungeonFloor);

            AddToInitiative(battleground.GetAliveCharactersFrom(false));

            round++;
            TryToStartTurn();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    int DidOnePartyLost()
    {
        if (battleground.GetTilesFromAliveCharactersOf(true).Count <= 0)
            return -1;
        if (battleground.GetTilesFromAliveCharactersOf(false).Count <= 0)
            return 1;
        return 0;
    }

    public static DungeonManager getInstance() { return instance; }
    public Battleground getBattleground() { return battleground; }
    public List<Character> getInitiativeOrder() { return initiativeOrder; }
    public int getRound() { return round; }

    void OnEnable()
    {
        EventManager.OnEndedTurn += NextTurn;
        EventManager.OnDeathOf += DeleteFromInitiative;
    }
    void OnDisable()
    {
        EventManager.OnEndedTurn -= NextTurn;
        EventManager.OnDeathOf -= DeleteFromInitiative;
    }
}