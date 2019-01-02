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
        hitted = attack > target.GetAttributes().GetStatValue(Attribute.Stats.Dodge);
    }
    public bool DidItHit() { return hitted; }
    public abstract int SortBestTargets(Character user, Character c1, Character c2);
}