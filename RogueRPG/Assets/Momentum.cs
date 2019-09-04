using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Momentum : MonoBehaviour
{
    /*
    AnimatedSlider animatedSlider;
    int currentMomentumDowntime = 0;
    int maxMomentumDowntime = 6;
    ParticleSystem particleSystem;

    void Awake()
    {
        animatedSlider = GetComponent<AnimatedSlider>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Stop();
    }

    public float Value
    {
        get
        {
            return animatedSlider.Value;
        }
        set
        {
            if (IsMomentumFull(true) || IsMomentumFull(false))
            {
                if (currentMomentumDowntime < maxMomentumDowntime)
                {
                    if (Random.value % 2 == 0)
                    {
                        MomentumCountdown();
                    }
                    return;
                }
            }
            animatedSlider.Value = value;
            if (animatedSlider.Value == animatedSlider.slider.maxValue)
                particleSystem.Play();
            else
                particleSystem.Stop();
        }
    }

    public void MomentumCountdown()
    {
        if (IsMomentumFull(true) || IsMomentumFull(false))
        {
            currentMomentumDowntime++;

            if (currentMomentumDowntime >= maxMomentumDowntime)
            {
                currentMomentumDowntime = 0;

                OnMomentumEquipUsed(Value == animatedSlider.slider.maxValue);
            }
        }
    }

    public void OnMomentumEquipUsed(bool side)
    {
        currentMomentumDowntime = maxMomentumDowntime;
        Value += side ? -(animatedSlider.slider.maxValue / 1.33334f) : animatedSlider.slider.maxValue / 1.33334f;
    }

    public bool IsMomentumFull(bool side)
    {
        return side ? Value == animatedSlider.slider.maxValue : Value == animatedSlider.slider.minValue;
    }
    */
}