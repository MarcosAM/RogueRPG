using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : MonoBehaviour/*, IPlaySkillAnimation */
{

    protected Animator animator;
    protected RectTransform rectTransform;
    //protected Equip mySkill;
    //protected Skill mySkillEffect;
    //protected IWaitForAnimation requester;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    //public void PlayAnimation(Equip skill, Tile tile)
    //{
    //    rectTransform.localPosition = tile.GetLocalPosition() + new Vector2(0, 20);
    //    mySkill = skill;
    //    animator.SetTrigger("play");
    //}

    //public void PlayAnimation(Skill skillEffect, Tile tile)
    //{
    //    rectTransform.localPosition = tile.GetLocalPosition() + new Vector2(0, 50);
    //    mySkillEffect = skillEffect;
    //    animator.SetTrigger("play");
    //}

    //public void PlayAnimation(IWaitForAnimation requester, Vector2 animationPosition)
    //{
    //    rectTransform.localPosition = animationPosition + new Vector2(0, 50);
    //    this.requester = requester;
    //    animator.SetTrigger("play");
    //}

    public virtual void PlayAnimation(Vector2 animationPosition)
    {
        //rectTransform.localPosition = animationPosition + new Vector2(0, 50);
        rectTransform.localPosition = animationPosition;
        animator.SetTrigger("play");
    }

    public virtual void End()
    {
        Destroy(gameObject);
    }
}
