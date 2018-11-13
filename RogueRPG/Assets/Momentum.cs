using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Momentum : MonoBehaviour
{

    float targetAmount = 0;
    float maxAmount = 100;
    [SerializeField] float fillSpeed;
    bool filling = false;
    Slider slider;
    int currentMomentumDowntime = 0;
    int maxMomentumDowntime = 4;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetTargetAmount(float value)
    {
        targetAmount += value;
        if (targetAmount > maxAmount)
            targetAmount = maxAmount;
        if (targetAmount < 0)
            targetAmount = 0;
        if (!filling)
        {
            StartCoroutine(AnimateSlider());
        }
    }

    IEnumerator AnimateSlider()
    {
        filling = true;
        float currentDisplayedAmount = slider.value * maxAmount;
        //TODO Delete the currentDisplayedAmount 
        while (currentDisplayedAmount != targetAmount)
        {
            currentDisplayedAmount = Mathf.MoveTowards(currentDisplayedAmount, targetAmount, Time.deltaTime * fillSpeed);
            slider.value = currentDisplayedAmount / maxAmount;
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

    public bool IsMomentumFull() { return slider.value == slider.maxValue; }

    public void OnMomentumSkillUsed()
    {
        SetTargetAmount(-maxAmount / 2);
    }

    //TODO Método para contar a cada turno que passa que talvez tenha que descer o momentum
}