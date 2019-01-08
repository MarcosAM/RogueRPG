using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Momentum : MonoBehaviour
{
    //TODO Método para contar a cada turno que passa que talvez tenha que descer o momentum

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
            currentMomentumDowntime = 0;

        return currentMomentumDowntime >= maxMomentumDowntime;
        //if (currentMomentumDowntime >= maxMomentumDowntime)
        //{
        //    currentMomentumDowntime = 0;
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }

    public void OnMomentumEquipUsed()
    {
        Value -= animatedSlider.slider.maxValue / 2;
    }

    public bool IsMomentumFull(bool side)
    {
        return side ? Value == animatedSlider.slider.maxValue : Value == animatedSlider.slider.minValue;
    }
}