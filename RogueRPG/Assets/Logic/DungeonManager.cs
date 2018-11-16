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
        battleground = GetComponent<Battleground>();
        GameManager gameManager = GameManager.getInstance();
        MakeItASingleton();

        List<Character> pcs = new List<Character>();
        for (int i = 0; i < gameManager.getHeroesStats().Count; i++)
        {
            Character character = gameManager.CreateCharacter(true, gameManager.getHeroesStats()[i]);
            character.setName(gameManager.getHeroesNames()[i]);
            pcs.Add(character);
        }

        battleground.ClearAndSetASide(pcs);
        battleground.ClearAndSetASide(gameManager.getEnemiesAtFloor(dungeonFloor));
        foreach (Character hero in battleground.getHeroSide())
        {
            if (hero != null)
                initiativeOrder.Add(hero);
        }
        foreach (Character enemy in battleground.getEnemySide())
        {
            if (enemy != null)
                initiativeOrder.Add(enemy);
        }
        foreach (Character character in initiativeOrder)
        {
            character.refresh();
        }
        print("Nós vamos para um battlegroun " + gameManager.GetBattlegroundSize(dungeonFloor));
        battleground.SetSize(gameManager.GetBattlegroundSize(dungeonFloor));
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

    public void addToInitiative(Character character)
    {
        initiativeOrder.Add(character);
    }

    void EndBattleAndCheckIfDungeonEnded()
    {
        dungeonFloor++;
        GameManager gameManager = GameManager.getInstance();
        if (dungeonFloor < gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups().Count)
        {
            AdvanceInitiative(initiativeOrder);
            battleground.ClearAndSetASide(gameManager.getEnemiesAtFloor(dungeonFloor));
            battleground.SetSize(gameManager.GetBattlegroundSize(dungeonFloor));

            //battleground.ShowCharactersToThePlayer();
            foreach (Character character in battleground.getEnemySide())
            {
                if (character != null)
                    initiativeOrder.Add(character);
            }
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
        int countdown = 0;
        for (int i = 0; i < battleground.getHeroSide().Count; i++)
        {
            if (battleground.getHeroSide()[i] != null)
            {
                if (!battleground.getHeroSide()[i].isAlive())
                {
                    countdown++;
                }
            }
        }
        if (countdown == battleground.HowManyCharacters(true))
        {
            return -1;
        }
        countdown = 0;
        for (int i = 0; i < battleground.getEnemySide().Count; i++)
        {
            if (battleground.getEnemySide()[i] != null)
            {
                if (!battleground.getEnemySide()[i].isAlive())
                {
                    countdown++;
                }
            }
        }
        if (countdown == battleground.HowManyCharacters(false))
        {
            return 1;
        }
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