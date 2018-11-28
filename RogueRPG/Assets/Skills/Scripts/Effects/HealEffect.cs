using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Heal")]
public class HealEffect : Effects
{
    [SerializeField]
    [Range(1f, 2f)]
    protected float healIntensifier;

    public override void Affect(Character user, Character target) { target.Heal((int)(user.GetStatValue(Stat.Stats.Atkm) * healIntensifier)); }
    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return (int)(c1.GetHp() - c2.GetHp());
    }
    public override bool IsLogicalTarget(Tile tile)
    {
        //TODO Pensar se é necessário impedir de curar aliados que estão regenerando
        return tile.GetCharacter() ? tile.GetCharacter().GetHp() != tile.GetCharacter().GetHp() : false;
    }
}