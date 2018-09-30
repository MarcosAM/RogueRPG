using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : Character
{
    protected override void FillStats()
    {
        base.FillStats();
        portrait = stats.getPortrait();
    }

    public override bool IsPlayable()
    {
        return false;
    }
}
