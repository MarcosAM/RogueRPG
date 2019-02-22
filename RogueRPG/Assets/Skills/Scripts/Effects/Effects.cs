using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effects : SkillEffect
{
    public abstract bool IsLogicalTarget(Tile tile);
    public abstract int GetComparableValue(Character character);

    public override void TryToAffect(Character user, Character target, float attack)
    {
        Affect(user, target);
    }
}