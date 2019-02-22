using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Debuff and Buff")]
public class DebuffNBuff : Debuff
{
    [SerializeField] protected int buffDuration;
    [SerializeField] protected Attribute.Type buffType;
    [SerializeField] protected Attribute.Intensity buffIntensity;

    protected override void OnHit(Character user, Character target)
    {
        base.OnHit(user, target);
        target.GetAttributes().BuffIt(buffType, buffIntensity, buffDuration);
    }

    public override void Affect(Character user, Character target)
    {
        TryToAffect(user, target, 100);
    }

    public override string GetEffectDescription() { return (intensity + " in " + type + " and " + buffIntensity + " in " + buffType); }

}
