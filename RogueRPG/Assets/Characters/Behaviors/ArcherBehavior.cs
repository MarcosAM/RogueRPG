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
        Battleground.Tile[] tempPCTiles = character.GetEnemiesTiles();
        Battleground.Tile[] tempNPCTiles = character.GetAlliesTiles();
        Equip choosedEquip = character.getUsableEquips()[Random.RandomRange(0,character.getUsableEquips().Count)];

        targetTile = tempPCTiles[character.getPosition()];
        if (!targetTile.IsOccupied())
        {
            for (int i = 0; i < tempPCTiles.Length; i++)
            {
                if (targetTile != null)
                {
                    if (tempPCTiles[i].getOccupant() != null)
                    {
                        if (targetTile.getOccupant() != null)
                        {
                            if (tempPCTiles[i].getOccupant().getHp() > targetTile.getOccupant().getHp())
                            {
                                targetTile = tempPCTiles[i];
                            }
                        }
                        else
                        {
                            targetTile = tempPCTiles[i];
                        }
                    }
                }
                else
                {
                    if (tempPCTiles[i].getOccupant() != null)
                    {
                        targetTile = tempPCTiles[i];
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
            if (maxIndex >= tempNPCTiles.Length)
                maxIndex = tempNPCTiles.Length - 1;
            for (int i = 0; i < tempNPCTiles.Length; i++)
            {
                currentDistance = 0;
                for (int j = 0; j < tempPCTiles.Length; j++)
                {
                    leftIndex = i - j;
                    if (leftIndex < minIndex)
                        leftIndex = 0;
                    rightIndex = i + j;
                    if (rightIndex > maxIndex)
                        rightIndex = maxIndex;
                    if (tempPCTiles[leftIndex].getOccupant() != null || tempPCTiles[rightIndex].getOccupant() != null)
                    {
                        break;
                    }
                    else
                    {
                        currentDistance++;
                    }
                }
                if (currentDistance > mostDistant || (currentDistance == mostDistant && (targetTile.getOccupant() != null && tempNPCTiles[i].getOccupant() == null)))
                {
                    targetTile = tempNPCTiles[i];
                    mostDistant = currentDistance;
                }
            }
        }
        UseSkill();

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
