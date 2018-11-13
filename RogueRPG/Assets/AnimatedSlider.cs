using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedSlider : MonoBehaviour
{
    public Slider slider;
    [SerializeField] float fillSpeed;
    bool filling = false;
    float tempValue;

    public float Value
    {
        get
        {
            return tempValue;
        }
        set
        {
            tempValue = value;
            if (tempValue > slider.maxValue)
                tempValue = slider.maxValue;
            if (tempValue < slider.minValue)
                tempValue = slider.minValue;
            if (!filling)
            {
                StartCoroutine(AnimateSlider());
            }
        }
    }

    void Awake()
    {
        slider = GetComponent<Slider>();
        tempValue = slider.value;
    }

    IEnumerator AnimateSlider()
    {
        filling = true;
        while (slider.value != tempValue)
        {
            slider.value = Mathf.MoveTowards(slider.value, tempValue, Time.deltaTime * fillSpeed);
            yield return new WaitForSeconds(0);
        }

        filling = false;
    }
}
