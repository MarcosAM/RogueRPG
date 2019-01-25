using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillToggle : StandartToggle
{
    private SkillToggleManager skillToggleManager;

    void Awake()
    {
        skillToggleManager = FindObjectOfType<SkillToggleManager>();

        FindComponents();
    }

    public void onToggleValueChange()
    {
        RefreshColor();
        skillToggleManager.OnAnyToggleChange(this);
    }
}