using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayableCharacter : Character
{
    protected override void FillStats()
    {
        base.FillStats();
        avatarImg.sprite = stats.GetSprite();
        avatarImg.color = stats.GetColor();
        //SetName(stats.GetName());
    }

    public override bool IsPlayable()
    {
        return false;
    }
}
