using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehavior : MonoBehaviour, IWaitForEquipment
{
    [SerializeField] protected Character character;
    protected Equip choosedEquip;
    protected Battleground.Tile targetTile;
    protected bool[] availableEquips;

    public virtual void StartTurn()
    {
        character.StartTurn();
        choosedEquip = null;
        targetTile = null;
        if (character.IsPlayable())
            FindObjectOfType<PlayerInputManager>().ShowUIFor(this);
        else
        {
            choosedEquip = character.getUsableEquips()[Random.Range(0, character.getUsableEquips().Count)];
            targetTile = choosedEquip.GetBestTarget(character);
            //TODO todos os golpes de inimigos não são momentum
            choosedEquip.UseEquipmentOn(character, targetTile, this, false);
        }
    }
    public virtual void UseEquip(int equip, Battleground.Tile target, bool momentum)
    {
        availableEquips[equip] = false;
        character.getEquips()[equip].UseEquipmentOn(character, target, this, momentum);
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
        availableEquips = new bool[character.getEquips().Length];
        for (int i = 0; i < availableEquips.Length; i++)
        {
            availableEquips[i] = true;
        }
        availableEquips[availableEquips.Length - 1] = false;
    }
    public virtual void ResumeFromEquipment()
    {
        character.getHUD().getAnimator().SetBool("Equiped", false);
        EndTurn();
    }

    void EndTurn()
    {
        choosedEquip = null;
        character.EndTurn();
    }
    public Character GetCharacter() { return character; }
    public Equip GetChoosedEquip() { return choosedEquip; }
    public void SetChoosedEquip(Equip skill) { this.choosedEquip = skill; }
    public void SetTargetTile(Battleground.Tile tile) { this.targetTile = tile; }
    public Battleground.Tile GetTargetTile() { return targetTile; }
    public bool IsEquipAvailable(int index)
    {
        if (index == availableEquips.Length - 1)
        {
            return DungeonManager.getInstance().isMomentumFull();
        }
        return availableEquips[index];
    }
    public bool AtLeastOneEquipAvailable()
    {
        foreach (bool b in availableEquips)
        {
            if (b)
                return true;
        }
        return false;
    }
    public void SetEquipsAvailability(bool availability)
    {
        for (int i = 0; i < availableEquips.Length; i++)
        {
            availableEquips[i] = availability;
        }
        availableEquips[availableEquips.Length - 1] = false;
    }
}

//for(int i = 0; i < possibleActions.Count;i++){
//	possibleActions [i].tryToDefineEquipSkillTargetFor();
//	if(choosedEquip != null && targetTile != null){
//		break;
//	}
//}
//if (targetTile == null) {
//	print ("Fuck!");
//} else {
//	UseSkill ();
//}
//		Battleground.Tile[] tempHeroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
//		Battleground.Tile[] tempEnemiesTiles = DungeonManager.getInstance ().getBattleground ().getEnemiesTiles ();
//
//		int maxIndex = character.getPosition() + choosedSkill.getMeleeEffect().getRange();
//		if (maxIndex >= tempHeroesTiles.Length)
//			maxIndex = tempHeroesTiles.Length - 1;
//		int minIndex = character.getPosition () - choosedSkill.getMeleeEffect().getRange ();
//		if (minIndex < 0)
//			minIndex = 0;
//
//		float hpFromLeft;
//		float hpFromRight;
//
//		for(int i = 0; i <= choosedSkill.getRange(); i++){
//			if (character.getPosition () + i <= maxIndex) {
//				if (tempHeroesTiles [character.getPosition () + i].getOccupant () != null)
//					hpFromRight = tempHeroesTiles [character.getPosition () + i].getOccupant ().getHp ();
//				else
//					hpFromRight = 0;
//			} else {
//				hpFromRight = 0;
//			}
//
//			if (character.getPosition () - i >= minIndex) {
//				if (tempHeroesTiles [character.getPosition () - i].getOccupant () != null)
//					hpFromLeft = tempHeroesTiles [character.getPosition () - i].getOccupant ().getHp ();
//				else
//					hpFromLeft = 0;
//			} else {
//				hpFromLeft = 0;
//			}
//
//			if(hpFromLeft > 0 || hpFromRight > 0){
//				if (hpFromLeft > hpFromRight) {
//					targetTile = tempHeroesTiles [character.getPosition () - 1];
//					character.getHUD ().UseSkillAnimation ();
//					return;
//				} else {
//					targetTile = tempHeroesTiles [character.getPosition () + 1];
//					character.getHUD ().UseSkillAnimation ();
//					return;
//				}
//			}
//		}
//
//		for(int i = 0; i <= tempHeroesTiles.Length; i++){
//			if (character.getPosition () + i < tempHeroesTiles.Length) {
//				if (tempHeroesTiles [character.getPosition () + i].getOccupant () != null)
//					hpFromRight = tempHeroesTiles [character.getPosition () + i].getOccupant ().getHp ();
//				else
//					hpFromRight = 0;
//			} else {
//				hpFromRight = 0;
//			}
//
//			if (character.getPosition () - i >= 0) {
//				if (tempHeroesTiles [character.getPosition () - i].getOccupant () != null)
//					hpFromLeft = tempHeroesTiles [character.getPosition () - i].getOccupant ().getHp ();
//				else
//					hpFromLeft = 0;
//			} else {
//				hpFromLeft = 0;
//			}
//			if(hpFromLeft > 0 || hpFromRight > 0){
//				if (hpFromLeft > hpFromRight) {
//					if (character.getPosition () - i >= minIndex) {
//						targetTile = tempHeroesTiles [character.getPosition () - i];
//						character.getHUD ().UseSkillAnimation ();
//						return;
//					} else {
//						if (character.getPosition () - i >= 0) {
//							//TODO fazer com que ele ande só no máximo a distância da skill de andar
//							targetTile = tempEnemiesTiles [character.getPosition () - i];
//							character.getHUD ().UseSkillAnimation ();
//							return;
//						}
//					}
//				} else {
//					if (character.getPosition () + i <= maxIndex) {
//						targetTile = tempHeroesTiles [character.getPosition () + i];
//						character.getHUD ().UseSkillAnimation ();
//						return;
//					} else {
//						if (character.getPosition () + i < tempEnemiesTiles.Length) {
//							targetTile = tempEnemiesTiles [character.getPosition () + i];
//							character.getHUD ().UseSkillAnimation ();
//							return;
//						}
//					}
//				}
//			}
//		}

//Battleground.Tile[] tempPCTiles = character.GetEnemiesTiles();
//Battleground.Tile[] tempNPCTiles = character.GetAlliesTiles();
//Equip choosedEquip = character.getUsableEquips()[Random.RandomRange(0,character.getUsableEquips().Count)];

//targetTile = tempPCTiles[character.getPosition()];
//if (!targetTile.IsOccupied())
//{
//    for (int i = 0; i < tempPCTiles.Length; i++)
//    {
//        if (targetTile != null)
//        {
//            if (tempPCTiles[i].getOccupant() != null)
//            {
//                if (targetTile.getOccupant() != null)
//                {
//                    if (tempPCTiles[i].getOccupant().getHp() > targetTile.getOccupant().getHp())
//                    {
//                        targetTile = tempPCTiles[i];
//                    }
//                }
//                else
//                {
//                    targetTile = tempPCTiles[i];
//                }
//            }
//        }
//        else
//        {
//            if (tempPCTiles[i].getOccupant() != null)
//            {
//                targetTile = tempPCTiles[i];
//            }
//        }
//    }
//}
//else
//{
//    int mostDistant = 0;
//    int currentDistance = 0;
//    int leftIndex = 0;
//    int rightIndex = 0;
//    int minIndex = character.getPosition() - choosedEquip.GetAlliesEffect().GetRange();
//    if (minIndex < 0)
//        minIndex = 0;
//    int maxIndex = character.getPosition() + choosedEquip.GetAlliesEffect().GetRange();
//    if (maxIndex >= tempNPCTiles.Length)
//        maxIndex = tempNPCTiles.Length - 1;
//    for (int i = 0; i < tempNPCTiles.Length; i++)
//    {
//        currentDistance = 0;
//        for (int j = 0; j < tempPCTiles.Length; j++)
//        {
//            leftIndex = i - j;
//            if (leftIndex < minIndex)
//                leftIndex = 0;
//            rightIndex = i + j;
//            if (rightIndex > maxIndex)
//                rightIndex = maxIndex;
//            if (tempPCTiles[leftIndex].getOccupant() != null || tempPCTiles[rightIndex].getOccupant() != null)
//            {
//                break;
//            }
//            else
//            {
//                currentDistance++;
//            }
//        }
//        if (currentDistance > mostDistant || (currentDistance == mostDistant && (targetTile.getOccupant() != null && tempNPCTiles[i].getOccupant() == null)))
//        {
//            targetTile = tempNPCTiles[i];
//            mostDistant = currentDistance;
//        }
//    }
//}
//UseSkill();