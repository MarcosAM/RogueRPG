using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action<Character> OnDeathOf;

    public static void DeathOf(Character c)
    {
        if (OnDeathOf != null)
        {
            OnDeathOf(c);
        }
    }
}
