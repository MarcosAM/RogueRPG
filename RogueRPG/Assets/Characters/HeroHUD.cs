using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHUD : CharacterHUD
{
    [SerializeField] Text hpText;
    [SerializeField] protected Text nameText;

    public override void Initialize(Character character)
    {
        nameText.text = character.Name;

        base.Initialize(character);
    }

    void SetHpNumbers(float hp, float maxHp)
    {
        hpText.text = hp.ToString();
    }

    protected override void Refresh()
    {
        base.Refresh();
        SetHpNumbers(hp.GetValue(), hp.GetMaxValue());
    }

    void OnEnable()
    {
        if (hp != null)
        {
            hp.OnHUDValuesChange += Refresh;
            hp.OnHPValuesChange += HPFeedback;
        }
    }

    void OnDisable()
    {
        if (hp != null)
        {
            hp.OnHUDValuesChange -= Refresh;
            hp.OnHPValuesChange -= HPFeedback;
        }
    }
}
