using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effects/Escudada")]
public class SEEscudada : SAtk
{
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;
    [SerializeField] int duration;
    [SerializeField] int secondaryRange;

    protected override void Effect()
    {
        Tile uniqueEffectTarget = FindObjectOfType<Battleground>().GetAvailableTiles().Find(t => UniqueEffectWillAffect(currentUser, currentTargetTile, t));
        List<Tile> secondaryEffectTargets = FindObjectOfType<Battleground>().GetAvailableTiles().FindAll(t => SecondaryEffectWillAffect(currentUser, currentTargetTile, t));
        targetsLeft = secondaryEffectTargets.Count + 1;
        EffectAnimation(uniqueEffectTarget);
        UniqueEffect(currentUser, uniqueEffectTarget);
        foreach (Tile secondaryTile in secondaryEffectTargets)
        {
            EffectAnimation(secondaryTile);
            SecondaryEffect(currentUser, secondaryTile);
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
        return user.IsPlayable() == tile.GetSide() ? Mathf.Abs(tile.GetRow() - user.getPosition()) <= secondaryRange && user != tile.GetCharacter() : false;
    }
}