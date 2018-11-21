using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions : ScriptableObject
{
    public abstract bool IsTargetable(Character user, Tile tile);
    public abstract bool WillBeAffected(Character user, Tile target, Tile tile);
    public abstract void Act(Character user, Tile target);
}
