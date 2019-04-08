using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Heal")]
public class HealEffect : Effects
{
    [SerializeField]
    [Range(0.5f, 1f)]
    protected float healIntensifier;

    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);
        target.GetAttributes().Heal((int)(target.GetAttributes().GetMaxHP() * healIntensifier));
    }
    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return (int)(c1.GetAttributes().GetHP() - c2.GetAttributes().GetHP());
    }
    public override bool IsLogicalTarget(Tile tile)
    {
        return tile.GetCharacter() ? tile.GetCharacter().GetAttributes().GetHP() != tile.GetCharacter().GetAttributes().GetHP() && tile.CharacterIs(true) : false;
    }
    public override int GetComparableValue(Character character)
    {
        float hpPercentege = character.GetAttributes().GetHP() / character.GetAttributes().GetHP();

        for (int i = TurnSugestion.maxProbability; i >= TurnSugestion.minProbability; i--)
        {
            if (i == 0)
                return i;
            if (hpPercentege > 1 - 1 / i)
                return i;
        }
        return 0;
    }

    public override string GetEffectDescription() { return "Heal " + healIntensifier * 100 + "% Magical Damage"; }
}