using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Momentum : MonoBehaviour
{
    //TODO Método para contar a cada turno que passa que talvez tenha que descer o momentum
    //TODO Enemies can flee to enabled tiles

    AnimatedSlider animatedSlider;
    public float Value
    {
        get
        {
            return animatedSlider.Value;
        }
        set
        {
            animatedSlider.Value = value;
        }
    }

    int currentMomentumDowntime = 0;
    [SerializeField] int maxMomentumDowntime = 4;

    void Awake()
    {
        animatedSlider = GetComponent<AnimatedSlider>();
    }

    public bool ShouldMomentumStop()
    {
        currentMomentumDowntime++;
        if (currentMomentumDowntime >= maxMomentumDowntime)
        {
            currentMomentumDowntime = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnMomentumSkillUsed()
    {
        Value -= animatedSlider.slider.maxValue / 2;
    }

    public bool IsMomentumFull() { return Value == animatedSlider.slider.maxValue; }
}