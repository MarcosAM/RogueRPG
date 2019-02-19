﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character/Stats")]
public class StandartStats : ScriptableObject
{
    [SerializeField] string cName;
    [SerializeField] bool playable;
    [SerializeField] private Equip[] skills;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Color color;
    [SerializeField] private bool usesEquip;

    public string GetName() { return cName; }
    public void SetName(string name) { cName = name; }
    public Equip[] GetEquips() { return skills; }
    public Sprite GetSprite() { return sprite; }
    public Color GetColor() { return color; }
    public bool GetPlayable() { return playable; }
    public bool UsesEquip() { return usesEquip; }
}