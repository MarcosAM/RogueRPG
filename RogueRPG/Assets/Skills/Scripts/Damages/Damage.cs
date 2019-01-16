using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : SkillEffect
{
    [SerializeField]
    [Range(0.8f, 5f)]
    protected float dmgIntensifier;
}