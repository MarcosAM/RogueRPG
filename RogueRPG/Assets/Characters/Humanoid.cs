﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Humanoid : Character
{

    protected RectTransform frontHandler;
    protected RectTransform backHandler;

    RectTransform[,] rectTransforms;

    protected override void PrepareRectTransforms()
    {
        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();

        avatarImg = GetComponentInChildren<Image>();
        backHandler = transforms[0];
        frontHandler = transforms[2];
    }

    public override void CreateEquipsSprites(Equip[] equips)
    {
        if (rectTransforms != null)
        {
            for (var i = 0; i < equips.Length; i++)
            {
                Destroy(rectTransforms[i, 0].gameObject);
                Destroy(rectTransforms[i, 1].gameObject);
            }
        }

        rectTransforms = new RectTransform[equips.Length, 2];

        for (var i = 0; i < equips.Length; i++)
        {
            if (equips[i].GetBackEquipPrefab())
                rectTransforms[i, 0] = Instantiate(equips[i].GetBackEquipPrefab());
            else
                rectTransforms[i, 0] = null;
            if (equips[i].GetFrontEquipPrefab())
                rectTransforms[i, 1] = Instantiate(equips[i].GetFrontEquipPrefab());
            else
                rectTransforms[i, 1] = null;
        }
    }

    public override void ChangeEquipObject(int equipIndex)
    {
        base.ChangeEquipObject(equipIndex);

        foreach (RectTransform child in frontHandler)
        {
            child.SetParent(null);
        }
        foreach (RectTransform child in backHandler)
        {
            child.SetParent(null);
        }

        if (rectTransforms[equipIndex, 0] != null)
        {
            rectTransforms[equipIndex, 0].SetParent(backHandler);
            rectTransforms[equipIndex, 0].anchoredPosition = new Vector2(0, 0);
            rectTransforms[equipIndex, 0].localEulerAngles = Vector3.zero;
        }
        if (rectTransforms[equipIndex, 1] != null)
        {
            rectTransforms[equipIndex, 1].SetParent(frontHandler);
            rectTransforms[equipIndex, 1].anchoredPosition = new Vector2(0, 0);
            rectTransforms[equipIndex, 1].localEulerAngles = Vector3.zero;
        }
    }

    public override void RemoveSelf()
    {
        if (rectTransforms != null)
        {
            for (var i = 0; i < rectTransforms.Length / 2; i++)
            {
                if (rectTransforms[i, 0])
                    Destroy(rectTransforms[i, 0].gameObject);
                if (rectTransforms[i, 1])
                    Destroy(rectTransforms[i, 1].gameObject);
            }
        }

        base.RemoveSelf();
    }
}