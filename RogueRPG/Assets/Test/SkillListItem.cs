using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillListItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text text;
    string tip;
    bool hover = false;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void Initialize(string text, string tip)
    {
        this.text.text = text;
        this.tip = tip;
        hover = true;
    }

    public void Initialize(string text)
    {
        this.text.text = text;
        hover = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hover)
            Tooltip.ShowTooltip(tip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hover)
            Tooltip.HideTooltip();
    }
}