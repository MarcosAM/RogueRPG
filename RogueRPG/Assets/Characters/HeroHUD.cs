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
        SetHpNumbers(attributes.GetHP(), attributes.GetMaxHP());
    }

    void OnEnable()
    {
        if (attributes != null)
        {
            attributes.OnHUDValuesChange += Refresh;
            attributes.OnHPValuesChange += HPFeedback;
        }
    }

    void OnDisable()
    {
        if (attributes != null)
        {
            attributes.OnHUDValuesChange -= Refresh;
            attributes.OnHPValuesChange -= HPFeedback;
        }
    }
}
