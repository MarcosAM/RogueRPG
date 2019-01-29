using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaySkillAnimation
{
    void PlayAnimation(IWaitForAnimation requester, Vector2 animationPosition);
    //	void playAnimation (IWaitForAnimation requester);
}

public interface IWaitForAnimation
{
    void ResumeFromAnimation();
}

public interface IPlayEffects
{
    void playEffect(IWaitForEffectsToEnd requester, Vector2 effectPosition);
}

public interface IWaitForEffectsToEnd
{
    void resumeFromEffect(IPlayEffects requester);
}

public interface IRegeneratable
{
    void startGeneration(int duration);
    void startGeneration();
}

public interface IPoisonable
{
    void getPoisoned();
}

public interface IPlayAnimationByString
{
    void PlayAnimation(IWaitForAnimationByString requester, string trigger);
}

public interface IWaitForAnimationByString
{
    void ResumeFromAnimation(IPlayAnimationByString animationByString);
}

public interface IWaitForEquipment
{
    void ResumeFromEquipment();
}

public interface IWaitForSkill
{
    void resumeFromSkill();
}