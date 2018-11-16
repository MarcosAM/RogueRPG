using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    public static event Action OnEndedTurn;
    public static event Action<Character> OnDeathOf;

    public static void EndedTurn()
    {
        if (OnEndedTurn != null)
            OnEndedTurn();
    }

    public static void DeathOf(Character c)
    {
        if (OnDeathOf != null)
        {
            OnDeathOf(c);
        }
    }
}
