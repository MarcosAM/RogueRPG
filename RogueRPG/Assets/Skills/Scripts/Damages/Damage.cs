using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : ScriptableObject
{
    [SerializeField]
    [Range(1f, 2f)]
    protected float dmgIntensifier;
    protected bool hitted;

    public virtual void TryToDamage(Character user, Character target, float attack)
    {
        hitted = attack > target.GetStatValue(Stat.Stats.Dodge);
    }
    public bool DidItHit() { return hitted; }
}