﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Heal")]
public class HealEffect : Effects
{
    [SerializeField]
    [Range(1f, 2f)]
    protected float healIntensifier;

    public override void Affect(Character user, Character target) { target.GetAttributes().Heal((int)(user.GetAttributes().GetStatValue(Attribute.Stats.Atkm) * healIntensifier)); }
    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return (int)(c1.GetAttributes().GetHp() - c2.GetAttributes().GetHp());
    }
    public override bool IsLogicalTarget(Tile tile)
    {
        //TODO Pensar se é necessário impedir de curar aliados que estão regenerando
        return tile.GetCharacter() ? tile.GetCharacter().GetAttributes().GetHp() != tile.GetCharacter().GetAttributes().GetHp() : false;
    }
    public override int GetComparableValue(Character character)
    {
        float hpPercentege = character.GetAttributes().GetHp() / character.GetAttributes().GetMaxHp();

        for (int i = TurnSugestion.maxProbability; i >= TurnSugestion.minProbability; i--)
        {
            if (i == 0)
                return i;
            if (hpPercentege > 1 - 1 / i)
                return i;
        }
        return 0;
    }
}