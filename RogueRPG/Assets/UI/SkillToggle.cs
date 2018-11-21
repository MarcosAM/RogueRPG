using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillToggle : MonoBehaviour
{
    private SkillToggleManager skillToggleManager;
    [SerializeField] private Image lines;
    [SerializeField] private Image hexFill;
    private Text text;
    private Toggle toggle;
    //TODO Adicionar standartGrey na classe Color
    private Color standartGrey;

    void Awake()
    {
        skillToggleManager = FindObjectOfType<SkillToggleManager>();
        text = GetComponentInChildren<Text>();
        toggle = GetComponentInChildren<Toggle>();
        standartGrey = lines.color;
        refreshColor();
    }

    public void onToggleValueChange()
    {
        refreshColor();
        skillToggleManager.OnAnyToggleChange(this);
    }

    void refreshColor()
    {
        if (toggle.interactable)
        {
            if (toggle.isOn)
            {
                lines.color = Color.white;
                hexFill.color = standartGrey;
                text.color = Color.white;
            }
            else
            {
                lines.color = standartGrey;
                hexFill.color = Color.white;
                text.color = standartGrey;
            }
        }
        else
        {
            lines.color = lines.color.lightGrey();
            text.color = text.color.lightGrey();
        }
    }

    public Toggle getToggle() { return toggle; }
    public Text getText() { return text; }
}
