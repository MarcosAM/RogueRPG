using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillName : MonoBehaviour
{
    Text text;
    float duration = 3f;
    float time = 0f;
    static SkillName instance;

    void Start()
    {
        instance = this;
        text = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public static void ShowSkillName(string skillName)
    {
        instance.Show(skillName);
    }

    void Show(string skillName)
    {
        text.text = skillName;

        if (gameObject.activeInHierarchy)
        {
            time = 0f;
        }
        else
        {
            gameObject.SetActive(true);
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        while (time < duration)
        {
            yield return null;
            time += Time.deltaTime;
        }
        time = 0f;
        gameObject.SetActive(false);
    }

}
