﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : Actions
{
    [SerializeField]
    [Range(0f, 1f)]
    protected float precision;
    protected float attack;

    protected void GenerateNewAttack(Character user)
    {
        attack = user.GetAttributes().GetPrecisionValue() + precision - Random.value;
    }
}