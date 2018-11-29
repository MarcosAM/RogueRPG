using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Assist and Move")]
public class SAssistMove : Skill
{
    [SerializeField]
    Assist assist;
    [SerializeField]
    Move move;

    public override bool IsTargetable(Character user, Tile tile) { return move.IsTargetable(user, tile); }
    //TODO rever isso aqui levando em consideração que com aura seria diferente. Pq o efeito acontece depois de se mexer.
    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return assist.WillBeAffected(user.GetTile(), target, tile) || move.WillBeAffected(user.GetTile(), target, tile); }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        if (assist is AuraAssist)
        {
            move.Act(user, tile, animationPrefab);
            assist.Act(user, tile, animationPrefab);
        }
        else
        {
            assist.Act(user, tile, animationPrefab);
            move.Act(user, tile, animationPrefab);
        }
    }

    //public override TurnSugestion GetTurnSugestion(Character user)
    //{
    //    List<Tile> alliesTiles = FindObjectOfType<Battleground>().GetAvailableTilesFrom(user.IsPlayable());
    //    List<Tile> targetableTiles = alliesTiles.FindAll(t => IsTargetable(user, t));
    //    targetableTiles.RemoveAll();
    //    targetableTiles.RemoveAll(t => alliesTiles.FindAll(t2 => UniqueEffectWillAffect(user, t, t2)).Exists(t3 => t3.CharacterIs(true) ? assist.GetEffect().GetComparableValue(t3.GetCharacter()) < 3 : false));
    //    //List<Tile> possibleTargets = allies.FindAll(t => IsTargetable(user, t) && assist.GetEffect().IsLogicalTarget(t));
    //    //if (possibleTargets.Count > 0)
    //    //{
    //    //    possibleTargets.Sort((t1, t2) => assist.GetEffect().SortBestTargets(user, t1.GetCharacter(), t2.GetCharacter()));
    //    //    Tile target = possibleTargets[0];
    //    //    return new TurnSugestion(TurnSugestion.maxProbability - allies.IndexOf(target), target.GetIndex());
    //    //}
    //    //else
    //    //    return new TurnSugestion(0);
    //}
}