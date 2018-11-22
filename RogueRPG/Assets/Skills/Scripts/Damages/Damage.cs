using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : ScriptableObject
{
    [SerializeField]
    [Range(1f, 2f)]
    protected float dmgIntensifier;

    public abstract void TryToDamage(Character user, Character target, float attack);
}
