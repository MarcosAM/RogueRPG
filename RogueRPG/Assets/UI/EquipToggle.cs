﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipToggle : StandartToggle
{
    private EquipToggleManager equipToggleManager;

    void Awake()
    {
        equipToggleManager = FindObjectOfType<EquipToggleManager>();

        FindComponents();
    }

    public void onToggleValueChange()
    {
        RefreshColor();
        equipToggleManager.OnAnyToggleChange(this);
    }
}
