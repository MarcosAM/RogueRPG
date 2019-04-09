﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject
{
    [SerializeField] protected SkillAnimation skillAnimationPrefab;

    protected bool hitted;




    public abstract int SortBestTargets(Character user, Character c1, Character c2);

    public virtual void Affect(Character user, Character target) { }





    public virtual void TryToAffect(Character user, Character target, float attack)
    {
        hitted = attack > target.GetAttributes().GetAgility();
    }

    public bool DidItHit() { return hitted; }





    public void EffectAnimation(Tile tile)
    {
        if (skillAnimationPrefab)
        {
            SkillAnimation skillAnimation = Instantiate(skillAnimationPrefab);
            skillAnimation.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
            skillAnimation.PlayAnimation(tile.GetLocalPosition());
        }
    }



    public abstract string GetEffectDescription();
}