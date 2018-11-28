using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TurnSugestion
{
    static public readonly int maxProbability = 5;
    static public readonly int minProbability = 0;
    public int probability;
    public int? targetPosition;

    public TurnSugestion(int probability, int? targetPosition = null)
    {
        this.probability = probability;
        this.targetPosition = targetPosition;
        if (this.probability < minProbability)
            this.probability = minProbability;
        if (this.probability > maxProbability)
            this.probability = maxProbability;
    }
}