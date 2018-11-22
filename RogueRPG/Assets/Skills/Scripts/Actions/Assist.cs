using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Assist : Actions
{
    [SerializeField]
    protected Effect effect;

    public Effect GetEffect() { return effect; }
}
