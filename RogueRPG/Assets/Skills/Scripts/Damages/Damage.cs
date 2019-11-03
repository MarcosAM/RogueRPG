using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : SkillEffect
{
    [SerializeField]
    [Range(0f, 7f)]
    protected float dmgIntensifier;
    protected int damage;

    public override void TryToAffect(Character user, Character target, float attack)
    {
        base.TryToAffect(user, target, attack);
        PrepareDamage(user, target);
        if (hitted)
            OnHit(user, target);
        else
            OnMiss(user, target);
    }

    protected abstract void PrepareDamage(Character user, Character target);
    protected abstract void OnHit(Character user, Character target);
    protected virtual void OnMiss(Character user, Character target)
    {
        target.GetAnimator().SetTrigger("Dodge");
    }

    public override void Affect(Character user, Character target)
    {
        TryToAffect(user, target, 100);
    }
}