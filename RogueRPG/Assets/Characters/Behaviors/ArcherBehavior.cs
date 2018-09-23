using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehavior : CombatBehavior
{

    void Start()
    {
        TurnAction atkHealthier = new TAAtkHealthier(this);
        TurnAction Retreat = new TARetreat(this);
        possibleActions.Add(atkHealthier);
        possibleActions.Add(Retreat);
    }

    public override void StartTurn()
    {
        base.StartTurn();
        character.StartTurn();
        ChooseSkill();
        ChooseTarget();
    }

    public void ChooseSkill()
    {
        choosedEquip = character.getUsableEquips()[Random.Range(0, character.getUsableEquips().Count)];
    }

    public void ChooseTarget()
    {
        Battleground.Tile[] tempHeroesTiles = character.GetEnemiesTiles();
        Battleground.Tile[] tempEnemiesTiles = character.GetAlliesTiles();
        Equip choosedEquip = character.getUsableEquips()[Random.RandomRange(0,character.getUsableEquips().Count)];

        targetTile = tempEnemiesTiles[character.getPosition()];
        if (!targetTile.IsOccupied())
        {
            for (int i = 0; i < tempHeroesTiles.Length; i++)
            {
                if (targetTile != null)
                {
                    if (tempHeroesTiles[i].getOccupant() != null)
                    {
                        if (tempHeroesTiles[i].getOccupant().getHp() > targetTile.getOccupant().getHp())
                        {
                            targetTile = tempHeroesTiles[i];
                        }
                    }
                }
                else
                {
                    if (tempHeroesTiles[i].getOccupant() != null)
                    {
                        targetTile = tempHeroesTiles[i];
                    }
                }
            }
        }
        else
        {
            int mostDistant = 0;
            int currentDistance = 0;
            int leftIndex = 0;
            int rightIndex = 0;
            int minIndex = character.getPosition() - choosedEquip.GetAlliesEffect().GetRange();
            if (minIndex < 0)
                minIndex = 0;
            int maxIndex = character.getPosition() + choosedEquip.GetAlliesEffect().GetRange();
            if (maxIndex >= tempEnemiesTiles.Length)
                maxIndex = tempEnemiesTiles.Length - 1;
            for (int i = 0; i < tempEnemiesTiles.Length; i++)
            {
                currentDistance = 0;
                for (int j = 0; j < tempHeroesTiles.Length; j++)
                {
                    leftIndex = i - j;
                    if (leftIndex < minIndex)
                        leftIndex = 0;
                    rightIndex = i + j;
                    if (rightIndex > maxIndex)
                        rightIndex = maxIndex;
                    if (tempHeroesTiles[leftIndex].getOccupant() != null || tempHeroesTiles[rightIndex].getOccupant() != null)
                    {
                        break;
                    }
                    else
                    {
                        currentDistance++;
                    }
                }
                if (currentDistance > mostDistant || (currentDistance == mostDistant && (targetTile.getOccupant() != null && tempEnemiesTiles[i].getOccupant() == null)))
                {
                    targetTile = tempEnemiesTiles[i];
                    mostDistant = currentDistance;
                }
            }
        }

        //for (int i = 0; i < possibleActions.Count; i++)
        //{
        //    possibleActions[i].tryToDefineEquipSkillTargetFor();
        //    if (choosedEquip != null && targetTile != null)
        //    {
        //        break;
        //    }
        //}
        //if (targetTile == null)
        //{
        //    print("Fuck!");
        //}
        //else
        //{
        //    UseSkill();
        //}
    }

    public override void UseSkill()
    {
        base.UseSkill();
        choosedEquip.UseEquipmentOn(character, targetTile, this);
    }

    public override void resumeFromEquipment()
    {
        EndTurn();
    }

    void EndTurn()
    {
        character.EndTurn();
    }
}
