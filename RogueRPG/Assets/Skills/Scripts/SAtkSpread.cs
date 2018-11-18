using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack and Spread")]
public class SAtkSpread : SAtk
{
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;
    [SerializeField] int duration;
    [SerializeField] int secondaryRange;
    protected int targetsLeft;

    protected override void Effect()
    {
        List<Tile> secondaryEffectTargets = FindObjectOfType<Battleground>().GetAvailableTiles().FindAll(t => SecondaryEffectWillAffect(currentUser, currentTargetTile, t));
        targetsLeft = secondaryEffectTargets.Count + 1;
        base.Effect();
        foreach (Tile secondaryTile in secondaryEffectTargets)
        {
            EffectAnimation(secondaryTile);
            SecondaryEffect(currentUser, secondaryTile);
        }
    }

    public override void ResumeFromAnimation()
    {
        targetsLeft--;
        if (targetsLeft <= 0)
        {
            EndSkill();
        }
    }

    protected void SecondaryEffect(Character user, Tile tile)
    {
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().BuffIt(stat, intensity, duration);
        }
    }

    public override TargetBtn.TargetBtnStatus GetTargetBtnStatus(Character user, Tile target, Tile tile, Equip equip)
    {
        if (UniqueEffectWillAffect(user, target, tile))
            return new TargetBtn.TargetBtnStatus(equip.GetSkillColor(this), ProbabilityToHit(user, target, tile));
        if (SecondaryEffectWillAffect(user, target, tile))
            return new TargetBtn.TargetBtnStatus(new Color(0.952f, 0.921f, 0.235f, 1));
        return new TargetBtn.TargetBtnStatus();
    }

    protected bool SecondaryEffectWillAffect(Character user, Tile target, Tile tile)
    {
        return user.IsPlayable() == tile.GetSide() ? Mathf.Abs(tile.GetRow() - user.GetPosition()) <= secondaryRange && user != tile.GetCharacter() : false;
    }
}