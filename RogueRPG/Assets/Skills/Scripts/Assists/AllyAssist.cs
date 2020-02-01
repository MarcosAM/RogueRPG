using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Ally")]
public class AllyAssist : Assist
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetRow() - tile.GetRow()) <= range && user.Playable == tile.GetSide(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }

    public override void Act(Character user, Tile target, SkillEffect skillEffect)
    {
        skillEffect.EffectAnimation(target);
        if ((skillEffect is ReviveEffect && target.CharacterIs(false)) || (!(skillEffect is ReviveEffect) && target.CharacterIs(true)))
            skillEffect.Affect(user, target.GetCharacter());
    }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground, SkillEffect skillEffect)
    {
        Effects effect = skillEffect as Effects;
        List<Tile> allies;

        if (skillEffect is ReviveEffect)
        {
            allies = battleground.GetTilesFromDeadCharactersOf(user.Playable);
        }
        else
        {
            allies = battleground.GetTilesFromAliveCharactersOf(user.Playable);
        }

        List<Tile> possibleTargets = allies.FindAll(t => IsTargetable(user, t) && effect.IsLogicalTarget(t));
        //TODO ver debuff. Talvez dar uma ênfase nos que já estão debuff e com nível igual. Idk
        if (possibleTargets.Count > 0)
        {
            allies.Sort((t1, t2) => effect.SortBestTargets(user, t1.GetCharacter(), t2.GetCharacter()));
            possibleTargets.Sort((t1, t2) => effect.SortBestTargets(user, t1.GetCharacter(), t2.GetCharacter()));
            Tile target = GetRandomTarget(possibleTargets);
            return new TurnSugestion(TurnSugestion.maxProbability - allies.IndexOf(target), target.GetIndex());
        }
        else
        {
            Debug.Log("Não faz sentido ajudar");
            return new TurnSugestion(0);
        }
    }

    public override string GetTargetDescription() { return "An ally. Range: " + range; }
}