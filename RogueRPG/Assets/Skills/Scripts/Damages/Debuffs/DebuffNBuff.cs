using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Debuff and Buff")]
public class DebuffNBuff : Debuff
{
    //TODO rever todas os Get Effect Description
    [SerializeField] protected int buffDuration;
    [SerializeField] protected Attributes.Attribute buffAttribute;

    protected override void OnHit(Character user, Character target)
    {
        base.OnHit(user, target);
        target.GetAttributes().StartEffect(buffAttribute, buffDuration);
    }

    public override string GetEffectDescription() { return (base.attribute + " and " + buffAttribute + " Debuff"); }

}
