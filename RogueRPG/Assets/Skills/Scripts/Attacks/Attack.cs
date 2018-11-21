using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [Range(0f, 1f)]
    protected float precision;
    [Range(0f, 1f)]
    protected float critic;
    [Range(0.1f, 1f)]
    protected float dmgIntensifier;
    protected int range;

    public abstract bool CanItReach(Character user, Tile target, Tile tile);
}
