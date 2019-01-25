using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandartToggle : MonoBehaviour
{
    [SerializeField] protected Image lines;
    [SerializeField] protected Image hexFill;
    protected Text text;
    protected Toggle toggle;
    //TODO Adicionar standartGrey na classe Color
    //TODO Deletar equiptoggle e skilltoggle. Eles não servem para nada e ver o animator dele também porque ele basicamente ficar chamando RefreshColor quase todo frame
    protected Color standartGrey;

    private void Awake()
    {
        FindComponents();
    }

    public void RefreshColor()
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

    protected void FindComponents()
    {
        text = GetComponentInChildren<Text>();
        toggle = GetComponentInChildren<Toggle>();
        standartGrey = lines.color;

        RefreshColor();
    }

    public Toggle getToggle() { return toggle; }
    public Text getText() { return text; }
    public void SetInterectable(bool interactable)
    {
        toggle.interactable = interactable;

        RefreshColor();
    }
}
