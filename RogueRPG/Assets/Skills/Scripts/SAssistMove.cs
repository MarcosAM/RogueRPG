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

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground)
    {
        TurnSugestion tsToAssist = GetTurnSugestionForAssisting(user, battleground);
        TurnSugestion tsToMove = move.GetTurnSugestion(user, battleground);

        Debug.Log("Prioridade de Assist and Move para Assistir é: " + tsToAssist.probability + " e para mover: " + tsToMove.probability);
        return tsToAssist.probability > tsToMove.probability ? tsToAssist : tsToMove;
    }

    TurnSugestion GetTurnSugestionForAssisting(Character user, Battleground battleground)
    {
        //TODO Só está pegando tiles de usuários vivos, vai ser inútil em uma skill de ressucitar
        List<Tile> alliesTiles = battleground.GetTilesFromAliveCharactersOf(user.IsPlayable());
        alliesTiles.RemoveAll(t => assist.GetEffect().GetComparableValue(t.GetCharacter()) < 0);

        if (alliesTiles.Count > 0)
        {
            List<Tile> targetableTiles = battleground.GetAvailableTilesFrom(user.IsPlayable()).FindAll(t => IsTargetable(user, t));

            if (targetableTiles.Count > 0)
            {
                targetableTiles.Sort((t1, t2) => alliesTiles.FindAll(a => UniqueEffectWillAffect(user, t2, a)).Count - alliesTiles.FindAll(a => UniqueEffectWillAffect(user, t1, a)).Count);

                //Teste
                var willBeAffected = alliesTiles.FindAll(a => UniqueEffectWillAffect(user, targetableTiles[0], a));
                var allValues = 0;
                foreach (Tile ally in willBeAffected)
                {
                    allValues += assist.GetEffect().GetComparableValue(ally.GetCharacter());
                }
                var probability = TurnSugestion.maxProbability - allValues / willBeAffected.Count;

                Debug.Log("A probabilidade de " + user.GetName() + " usar uma skill de Assist and Move é: " + probability);
                return new TurnSugestion(probability, targetableTiles[0].GetIndex());
                //Teste
            }
        }

        Debug.Log("Não faz sentido para " + user.GetName() + " usar uma skill de Assist and Move.");
        return new TurnSugestion(0);
    }
}