using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPStat : Attribute
{
    public HPStat(Character character, Stats stats, IBuffHUD buffHUD) : base(character, stats, buffHUD)
    {
        this.character = character;
        this.stats = stats;
        this.buffHUD = buffHUD;
    }

    public override void SpendAndCheckIfEnded()
    {
        character.GetAttributes().Heal((int)(character.GetAttributes().GetMaxHp() * getBuffValue()));
        base.SpendAndCheckIfEnded();
    }
}