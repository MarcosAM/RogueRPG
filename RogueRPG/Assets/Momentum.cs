using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Momentum : MonoBehaviour
{
    //TODO Método para contar a cada turno que passa que talvez tenha que descer o momentum
    //TODO Delete the currentDisplayedAmount
    //TODO Enemies can flee to enabled tiles

    Slider slider;
    [SerializeField] float fillSpeed;
    bool filling = false;
    float tempValue;

    int currentMomentumDowntime = 0;
    [SerializeField] int maxMomentumDowntime = 4;

    void Awake()
    {
        slider = GetComponent<Slider>();
        tempValue = slider.value;
    }

    public void AddMomentum(float value)
    {
        tempValue += value;
        if (tempValue > slider.maxValue)
            tempValue = slider.maxValue;
        if (tempValue < slider.minValue)
            tempValue = slider.minValue;
        if (!filling)
        {
            StartCoroutine(AnimateSlider());
        }
    }

    IEnumerator AnimateSlider()
    {
        filling = true;
        Debug.Log(slider.value);
        while (slider.value != tempValue)
        {
            slider.value = Mathf.MoveTowards(slider.value, tempValue, Time.deltaTime * fillSpeed);
            Debug.Log(slider.value);
            yield return new WaitForSeconds(0);
        }

        filling = false;
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
        AddMomentum(-slider.maxValue / 2);
    }

    public bool IsMomentumFull() { return slider.value == slider.maxValue; }
}