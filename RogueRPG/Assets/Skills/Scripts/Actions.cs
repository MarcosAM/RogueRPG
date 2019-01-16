using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions : ScriptableObject
{
    [SerializeField]
    [Range(0, 5)]
    protected int range;
    public abstract bool IsTargetable(Character user, Tile tile);
    public abstract bool WillBeAffected(Tile user, Tile target, Tile tile);
    public abstract void Act(Character user, Tile target, SkillEffect skillEffect);

    public abstract TurnSugestion GetTurnSugestion(Character user, Battleground battleground, SkillEffect skillEffect);
    protected Tile GetRandomTarget(List<Tile> possibleTargets)
    {
        if (possibleTargets.Count == 1)
            return possibleTargets[0];
        else
        {
            if (Random.value < 0.65f)
            {
                return possibleTargets[0];
            }
            else
            {
                //TODO test Random.Range(1,1)
                return possibleTargets[Random.Range(1, possibleTargets.Count - 1)];
            }
        }
    }

    public abstract string GetTargetDescription();
    public int GetRange() { return range; }
}