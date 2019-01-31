using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Assist and Move")]
public class SAssistMove : Movement
{
    [SerializeField]
    Assist assist;
    [SerializeField]
    Effects effect;

    public override bool IsTargetable(Character user, Tile tile) { return move.IsTargetable(user, tile); }
    //TODO rever isso aqui levando em consideração que com aura seria diferente. Pq o efeito acontece depois de se mexer.
    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return assist.WillBeAffected(user.GetTile(), target, tile) || move.WillBeAffected(user.GetTile(), target, tile); }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        if (assist is AuraAssist)
        {
            move.Act(user, tile, skillEffect);
            assist.Act(user, tile, skillEffect);
        }
        else
        {
            assist.Act(user, tile, skillEffect);
            move.Act(user, tile, skillEffect);
        }
    }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground)
    {
        TurnSugestion tsToAssist = GetTurnSugestionForAssisting(user, battleground);
        TurnSugestion tsToMove = move.GetTurnSugestion(user, battleground, skillEffect);

        return tsToAssist.probability > tsToMove.probability ? tsToAssist : tsToMove;
    }

    TurnSugestion GetTurnSugestionForAssisting(Character user, Battleground battleground)
    {
        //TODO Só está pegando tiles de usuários vivos, vai ser inútil em uma skill de ressucitar
        List<Tile> alliesTiles = battleground.GetTilesFromAliveCharactersOf(user.Playable);
        alliesTiles.RemoveAll(t => effect.GetComparableValue(t.GetCharacter()) < 0);

        if (alliesTiles.Count > 0)
        {
            List<Tile> targetableTiles = battleground.GetAvailableTilesFrom(user.Playable).FindAll(t => IsTargetable(user, t));

            if (targetableTiles.Count > 0)
            {
                targetableTiles.Sort((t1, t2) => alliesTiles.FindAll(a => UniqueEffectWillAffect(user, t2, a)).Count - alliesTiles.FindAll(a => UniqueEffectWillAffect(user, t1, a)).Count);

                //Teste
                var willBeAffected = alliesTiles.FindAll(a => UniqueEffectWillAffect(user, targetableTiles[0], a));
                var allValues = 0;
                foreach (Tile ally in willBeAffected)
                {
                    allValues += effect.GetComparableValue(ally.GetCharacter());
                }
                var probability = TurnSugestion.maxProbability - allValues / willBeAffected.Count;

                return new TurnSugestion(probability, targetableTiles[0].GetIndex());
                //Teste
            }
        }

        return new TurnSugestion(0);
    }

    public override string GetDescription() { return description + "\n \n" + "Target: " + move.GetTargetDescription() + "\n" + "Effect: Move and " + skillEffect.GetEffectDescription(); }
}