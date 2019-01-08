using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character/Stats")]
public class StandartStats : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] private Equip[] skills;
    [SerializeField] private Image portrait;

    public string GetName() { return name; }
    public Equip[] GetEquips() { return skills; }
    public Image GetPortrait() { return portrait; }
}