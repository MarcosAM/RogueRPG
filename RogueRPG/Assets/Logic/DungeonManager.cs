using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class DungeonManager : MonoBehaviour
{
    //TODO implement a Give Up Btn
    static DungeonManager instance = null;
    static int lastDungeonLevel;
    List<Character> initiativeOrder = new List<Character>();
    [SerializeField] Character characterPrefab;
    int round;
    int dungeonFloor = 0;
    Battleground battleground;
    TurnManager turnManager;
    Momentum momentum;
    BattleGroup[] battleGroups;

    WaitForSeconds delayNextTurn = new WaitForSeconds(1.8f);

    void Start()
    {
        battleground = FindObjectOfType<Battleground>();
        turnManager = GetComponent<TurnManager>();
        GameManager gameManager = GameManager.getInstance();
        momentum = FindObjectOfType<Momentum>();
        MakeItASingleton();

        List<Character> pcs = new List<Character>();
        for (int i = 0; i < PartyManager.GetParty().Length; i++)
        {
            pcs.Add(CreateCharacter(PartyManager.GetParty()[i]));
        }

        lastDungeonLevel = gameManager.GetCurrentDungeon().GetLevel();

        battleGroups = gameManager.GetCurrentDungeon().GetRandomBattleGroups();

        var npcs = new List<Character>();
        var npcsStats = battleGroups[0].GetEnemiesStats();

        foreach (var npcStat in npcsStats)
        {
            if (npcStat)
            {
                npcs.Add(CreateCharacter(npcStat));
            }
        }

        battleground.SetAvailableSide(pcs);
        battleground.SetAvailableSide(npcs);

        AddToInitiative(battleground.GetAliveCharactersFrom(true));
        AddToInitiative(battleground.GetAliveCharactersFrom(false));

        foreach (Character character in initiativeOrder)
        {
            character.GetAttributes().Refresh();
        }

        battleground.Size = battleGroups[0].GetBattlegroundSize();

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
        momentum.MomentumCountdown();
        turnManager.StartTurn(initiativeOrder[0]);
    }

    public IEnumerator NextTurn()
    {
        yield return delayNextTurn;
        if (DidOnePartyLost() > 0)
        {
            EndBattleAndCheckIfDungeonEnded();
            yield break;
        }
        if (DidOnePartyLost() == 0)
        {
            if (initiativeOrder.Count > 0)
            {
                AdvanceInitiative(initiativeOrder);
                round++;
                TryToStartTurn();
                yield break;
            }
        }
        if (DidOnePartyLost() < 0)
        {
            SceneManager.LoadScene(2);
            yield break;
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
        if (dungeonFloor < battleGroups.Length)
        {
            AdvanceInitiative(initiativeOrder);

            var enemies = FindObjectsOfType<Character>().Where(e => !e.Playable);
            foreach (var enemy in enemies)
            {
                enemy.RemoveSelf();
            }

            //var battleGroup = gameManager.GetCurrentDungeon().GetBattleGroup(dungeonFloor);

            battleground.Size = battleGroups[dungeonFloor].GetBattlegroundSize();

            var npcs = new List<Character>();
            var npcsStats = battleGroups[dungeonFloor].GetEnemiesStats();
            foreach (var npcStat in npcsStats)
            {
                if (npcStat)
                {
                    npcs.Add(CreateCharacter(npcStat));
                }
            }
            battleground.SetAvailableSide(npcs);

            AddToInitiative(battleground.GetAliveCharactersFrom(false));

            round++;
            TryToStartTurn();
        }
        else
        {
            gameManager.UnlockDungeons();
            SceneManager.LoadScene(4);
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

    Character CreateCharacter(StandartStats stats)
    {
        Character character = Instantiate(characterPrefab);
        character.SetStats(stats);
        return character;
    }

    public static DungeonManager getInstance() { return instance; }
    public Battleground getBattleground() { return battleground; }
    public List<Character> getInitiativeOrder() { return initiativeOrder; }
    public int GetRound() { return round; }
    public static int GetLastDungeonLevel() { return lastDungeonLevel; }

    void OnEnable()
    {
        EventManager.OnDeathOf += DeleteFromInitiative;
    }
    void OnDisable()
    {
        EventManager.OnDeathOf -= DeleteFromInitiative;
    }
}