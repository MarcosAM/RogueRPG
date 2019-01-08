﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour, IWaitForEquipment
{
    Character currentCharacter;
    bool momentumTurn;
    DungeonManager dungeonManager;
    PlayerInputManager playerInputManager;

    void Awake()
    {
        dungeonManager = GetComponent<DungeonManager>();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    public virtual void StartTurn(Character character)
    {
        this.currentCharacter = character;

        character.GetAttributes().SpendBuffs();
        character.GetInventory().CheckIfEquipsShouldBeRefreshed();

        if (!character.GetAttributes().GetHp().IsAlive())
        {
            dungeonManager.NextTurn();
            return;
        }

        if (character.IsPlayable())
            playerInputManager.ShowUIFor(currentCharacter);
        else
        {
            var index = Random.Range(0, character.GetInventory().GetUsableEquips().Count);
            momentumTurn = character.GetInventory().IsMomentumEquip(character.GetInventory().GetUsableEquips()[index]);
            character.GetInventory().GetUsableEquips()[index].UseEquipment(character, this);

            //TODO tem personagem se movendo além do range
            //TODO ter algum tipo de probabilidade ou algo do tipo para sair do momentum. Talvez ter 4 turnos para usar, mas cada golpe ou coisa parecida tem chance de tirar um turno do tempo do momentum.

            //TODO melhorar essa seboseira!
            var j = 0;
            for (int i = 0; i < currentCharacter.GetInventory().GetAvailableEquips().Length; i++)
            {
                if (currentCharacter.GetInventory().GetAvailableEquips()[i])
                {
                    if (j == index)
                    {
                        currentCharacter.GetInventory().GetAvailableEquips()[i] = false;
                        return;
                    }
                    j++;
                }
            }
        }
    }
    public virtual void UseEquip(int equipIndex, Tile target, int skill)
    {
        //TODO não pedir o equipamento inteiro, só pedir o número
        momentumTurn = currentCharacter.GetInventory().IsMomentumEquip(currentCharacter.GetInventory().GetEquips()[equipIndex]);
        currentCharacter.GetInventory().GetAvailableEquips()[equipIndex] = false;
        currentCharacter.GetInventory().GetEquips()[equipIndex].UseEquipmentOn(currentCharacter, target, this, skill);
    }

    public virtual void ResumeFromEquipment()
    {
        if (momentumTurn)
            FindObjectOfType<Momentum>().OnMomentumEquipUsed();
        dungeonManager.NextTurn();
    }
}