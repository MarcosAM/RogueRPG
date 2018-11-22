﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : Actions
{
    [SerializeField]
    [Range(0f, 1f)]
    protected float precision;
    protected float attack;
    [SerializeField]
    protected Damage damage;

    protected void GenerateNewAttack(Character user)
    {
        attack = user.GetStatValue(Stat.Stats.Precision) + precision - Random.value;
    }
}