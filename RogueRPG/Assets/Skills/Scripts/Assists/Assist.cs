using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Assist : Actions
{
    [SerializeField]
    protected Effects effect;

    public Effects GetEffect() { return effect; }
}
