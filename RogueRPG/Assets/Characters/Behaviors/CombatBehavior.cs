using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehavior : MonoBehaviour, IWaitForEquipment
{
    [SerializeField] protected Character character;
    protected Equip choosedEquip;
    protected Tile targetTile;
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
            choosedEquip = character.GetUsableEquips()[Random.Range(0, character.GetUsableEquips().Count)];
            //TODO Define target and skill
            //targetTile = choosedEquip.GetBestTarget(character);
            //TODO rever isso aqui
            //choosedEquip.UseEquipmentOn(character, targetTile, this, false);

            //TODO Está sempre como se fosse momentum false
            StartCoroutine(choosedEquip.ThinkAboutStuff(character,false,this));
            //choosedEquip.UseEquipment(character, this, false);
        }
    }
    public virtual void UseEquip(int equip, Tile target, bool momentum, int skill)
    {
        availableEquips[equip] = false;
        character.GetEquips()[equip].UseEquipmentOn(character, target, this, momentum, skill);
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
        availableEquips = new bool[character.GetEquips().Length];
        for (int i = 0; i < availableEquips.Length; i++)
        {
            availableEquips[i] = true;
        }
        availableEquips[availableEquips.Length - 1] = false;
    }
    public virtual void ResumeFromEquipment()
    {
        //character.getHUD().getAnimator().SetBool("Equiped", false);
        character.getAnimator().SetBool("Equiped", false);
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
    public void SetTargetTile(Tile tile) { this.targetTile = tile; }
    public Tile GetTargetTile() { return targetTile; }
    public bool IsEquipAvailable(int index)
    {
        if (index == availableEquips.Length - 1)
        {
            return FindObjectOfType<Momentum>().IsMomentumFull();
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