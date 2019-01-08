using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour
{

    Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void UpdateDescription(string description)
    {
        gameObject.SetActive(true);

        text.text = description;
    }

    public void HideDescription()
    {
        gameObject.SetActive(false);
    }
}
