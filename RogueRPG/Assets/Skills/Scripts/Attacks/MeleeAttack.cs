using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Melee")]
public class MeleeAttack : Attack
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetRow() - tile.GetRow()) <= range && tile.CharacterIs(true) && user.IsPlayable() != tile.GetSide(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }

    public override void Act(Character user, Tile target)
    {
        GenerateNewAttack(user);
        if (target.CharacterIs(true))
            damage.TryToDamage(user, target.GetCharacter(), attack);
    }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground)
    {
        List<Tile> enemies = battleground.GetTilesFromAliveCharactersOf(!user.IsPlayable());
        List<Tile> possibleTargets = enemies.FindAll(t => IsTargetable(user, t));

        if (possibleTargets.Count > 0)
        {
            enemies.Sort((t1, t2) => damage.SortBestTargets(user, t1.GetCharacter(), t2.GetCharacter()));
            possibleTargets.Sort((t1, t2) => damage.SortBestTargets(user, t1.GetCharacter(), t2.GetCharacter()));
            Tile target = GetRandomTarget(possibleTargets);

            Debug.Log(" A probabilidade de " + user.GetName() + " usar um Melee Attack é: " + (TurnSugestion.maxProbability - enemies.IndexOf(target)) + ". E deveria acetar a casa: " + target.GetIndex());
            return new TurnSugestion(TurnSugestion.maxProbability - enemies.IndexOf(target), target.GetIndex());
        }

        Debug.Log("Usar uma skill de Melee Attack não faz sentido para " + user.GetName());
        return new TurnSugestion(0);
    }
}