using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Heal")]
public class HealEffect : Effects
{
    [SerializeField]
    [Range(1f, 2f)]
    protected float healIntensifier;

    public override void Affect(Character user, Character target) { target.Heal((int)(user.GetStatValue(Stat.Stats.Atkm) * healIntensifier)); }
}