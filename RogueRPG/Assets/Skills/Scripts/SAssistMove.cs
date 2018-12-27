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

    public override TurnSugestion GetTurnSugestion(Character user)
    {
        List<Tile> alliesTiles = FindObjectOfType<Battleground>().GetTilesFromAliveCharactersOf(user.IsPlayable());
        alliesTiles.RemoveAll(t => assist.GetEffect().GetComparableValue(t.GetCharacter()) < 0);
        if (alliesTiles.Count > 0)
        {
            List<Tile> targetableTiles = FindObjectOfType<Battleground>().GetAvailableTilesFrom(user.IsPlayable()).FindAll(t => IsTargetable(user, t));
            if (targetableTiles.Count > 0)
            {
                targetableTiles.Sort((t1, t2) => alliesTiles.FindAll(a => UniqueEffectWillAffect(user, t2, a)).Count - alliesTiles.FindAll(a => UniqueEffectWillAffect(user, t1, a)).Count);
                return new TurnSugestion(TurnSugestion.maxProbability - (alliesTiles.Count - alliesTiles.FindAll(a => UniqueEffectWillAffect(user,targetableTiles[0],a)).Count), targetableTiles[0].GetIndex());
            }
        }
        return new TurnSugestion(0);
    }
}