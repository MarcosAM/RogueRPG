using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : Actions
{
    [SerializeField]
    [Range(0f, 1f)]
    protected float precision;


    protected float GenerateNewAttack(Character user)
    {
        return user.GetAttributes().GetAgility() + precision - Random.value;
    }
}