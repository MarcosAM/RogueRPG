using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : Character
{
    protected override void FillStats()
    {
        base.FillStats();
        avatarImg.sprite = stats.getPortrait().sprite;
    }

    public override bool IsPlayable()
    {
        return false;
    }
}
