using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayableCharacter : Character
{
    protected override void FillStats()
    {
        base.FillStats();
        avatarImg.sprite = stats.getPortrait().sprite;
        //transform.localScale = new Vector3(-1, 1, 1);
        //GetComponentsInChildren<Transform>()[1].localScale = new Vector3(-1, 1, 1);
    }

    public override void ChangeEquipObject(Image backEquip, Image frontEquip)
    {
        base.ChangeEquipObject(backEquip, frontEquip);
        backEquip.rectTransform.localScale = new Vector3(-1, 1, 1);
        frontEquip.rectTransform.localScale = new Vector3(-1, 1, 1);
    }

    public override bool IsPlayable()
    {
        return false;
    }
}
