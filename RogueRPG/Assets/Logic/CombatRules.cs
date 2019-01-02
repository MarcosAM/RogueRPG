using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatRules
{

    public static bool IsInRange(Tile user, Tile target, int range)
    {
        return GetDistance(user, target) <= range;
    }

    public static int GetDistance(Tile t1, Tile t2)
    {
        return Mathf.Abs(t1.GetRow() - t2.GetRow());
    }

}