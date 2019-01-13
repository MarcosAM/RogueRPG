using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : ScriptableObject
{
    [SerializeField]
    [Range(0.8f, 5f)]
    protected float dmgIntensifier;
    protected bool hitted;

    public virtual void TryToDamage(Character user, Character target, float attack)
    {
        EffectAnimation(target.GetTile());
        hitted = attack > target.GetAttributes().GetDodgeValue();
    }
    public bool DidItHit() { return hitted; }
    public abstract int SortBestTargets(Character user, Character c1, Character c2);

    [SerializeField] protected SkillAnimation skillAnimationPrefab;

    protected void EffectAnimation(Tile tile)
    {
        if (skillAnimationPrefab)
        {
            SkillAnimation skillAnimation = Instantiate(skillAnimationPrefab);
            skillAnimation.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
            skillAnimation.PlayAnimation(tile.GetLocalPosition());
        }
    }
}