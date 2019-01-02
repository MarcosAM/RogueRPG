using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour, IWaitForEquipment
{
    protected Character character;
    protected Equip choosedEquip;
    protected Tile targetTile;

    public virtual void StartTurn(Character character)
    {
        this.character = character;

        //character.StartTurn();

        character.SpendBuffs();
        character.CheckIfSkillsShouldBeRefreshed();

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
            //StartCoroutine(choosedEquip.ThinkAboutStuff(character,false,this));
            choosedEquip.UseEquipment(character, this, false);
        }
    }
    public virtual void UseEquip(int equipIndex, Tile target, bool momentum, int skill)
    {
        character.GetAvailableEquips()[equipIndex] = false;
        character.GetEquips()[equipIndex].UseEquipmentOn(character, target, this, momentum, skill);
    }

    public virtual void ResumeFromEquipment()
    {
        character.getAnimator().SetBool("Equiped", false);
        EndTurn();
    }

    void EndTurn()
    {
        choosedEquip = null;
        EventManager.EndedTurn();
    }
    public Character GetCharacter() { return character; }
    public Equip GetChoosedEquip() { return choosedEquip; }
    public void SetChoosedEquip(Equip skill) { this.choosedEquip = skill; }
    public void SetTargetTile(Tile tile) { this.targetTile = tile; }
    public Tile GetTargetTile() { return targetTile; }
}