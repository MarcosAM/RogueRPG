﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class DungeonManager : MonoBehaviour
{
    //TODO implement a Give Up Btn
    static DungeonManager instance = null;
    List<Character> initiativeOrder = new List<Character>();
    int round;
    int dungeonFloor = 0;
    Battleground battleground;
    TurnManager turnManager;
    BattleGroup[] battleGroups;

    WaitForSeconds delayNextTurn = new WaitForSeconds(1.8f);

    void Start()
    {
        battleground = FindObjectOfType<Battleground>();
        turnManager = GetComponent<TurnManager>();
        GameManager gameManager = GameManager.getInstance();
        MakeItASingleton();

        List<Character> pcs = new List<Character>();
        for (int i = 0; i < PartyManager.GetParty().Length; i++)
        {
            pcs.Add(PartyManager.GetParty()[i].GetCharacter());
        }

        battleGroups = gameManager.GetCurrentDungeon().GetBattleGroups();

        var npcs = new List<Character>();
        var npcsStats = battleGroups[0].GetEnemiesStats();

        foreach (var npcStat in npcsStats)
        {
            if (npcStat)
            {
                npcs.Add(npcStat.GetCharacter());
            }
            else
            {
                npcs.Add(null);
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

            battleground.Size = battleGroups[dungeonFloor].GetBattlegroundSize();

            var npcs = new List<Character>();
            var npcsStats = battleGroups[dungeonFloor].GetEnemiesStats();
            foreach (var npcStat in npcsStats)
            {
                if (npcStat)
                {
                    npcs.Add(npcStat.GetCharacter());
                }
                else
                {
                    npcs.Add(null);
                }
            }
            battleground.SetAvailableSide(npcs);

            AddToInitiative(battleground.GetAliveCharactersFrom(false));

            round++;
            TryToStartTurn();
        }
        else
        {
            gameManager.OnDungeonEnded();
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
    public int GetRound() { return round; }

    void OnEnable()
    {
        EventManager.OnDeathOf += DeleteFromInitiative;
    }
    void OnDisable()
    {
        EventManager.OnDeathOf -= DeleteFromInitiative;
    }
}