using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effects : ScriptableObject
{
    public abstract void Affect(Character user, Character target);
}