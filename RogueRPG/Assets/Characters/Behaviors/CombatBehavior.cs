using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehavior : MonoBehaviour, IWaitForEquipment
{
    [SerializeField] protected Character character;
    protected Equip choosedEquip;
    protected Battleground.Tile targetTile;
    protected List<TurnAction> possibleActions = new List<TurnAction>();
    protected bool[] availableEquips;

    public virtual void StartTurn() { }
    public virtual void UseSkill() { }
    public virtual void useEquip(int equip, Battleground.Tile target) { }
    public virtual void skillBtnPressed(Equip skill) { }
    public virtual void skillBtnPressed(int index) { }
    public virtual void targetBtnPressed(Battleground.Tile targetTile) { }
    public virtual void unchooseSkill() { }

    public void setCharacter(Character character)
    {
        this.character = character;
        availableEquips = new bool[character.getEquips().Length];
        for (int i = 0; i < availableEquips.Length; i++)
        {
            availableEquips[i] = true;
        }
        if (possibleActions != null)
        {
            for (int i = 0; i < possibleActions.Count; i++)
            {
                possibleActions[i].setCharacter(character);
            }
        }
    }
    public virtual void resumeFromEquipment() { }
    public Character getCharacter() { return character; }
    public Equip getChoosedSkill() { return choosedEquip; }
    public void setChoosedSkill(Equip skill) { this.choosedEquip = skill; }
    public void setTargetTile(Battleground.Tile tile) { this.targetTile = tile; }
    public Battleground.Tile getTargetTile() { return targetTile; }
    public bool IsEquipAvailable(int index) { return availableEquips[index]; }
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
        for(int i = 0;i < availableEquips.Length; i++)
        {
            availableEquips[i] = availability;
        }
    }
}