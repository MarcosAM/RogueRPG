using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatRules
{

    public static bool IsInRange(Tile user, Tile target, int range)
    {
        return Mathf.Abs(user.GetRow() - target.GetRow()) <= range;
    }

}