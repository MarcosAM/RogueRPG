using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{

    Character character;
    Slider hpSlider;
    [SerializeField] Text hpText;
    [SerializeField] private DamageFB damageFbPrefab;
    [SerializeField] private Text nameText;

    void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
        if (!this.character.IsPlayable())
        {
            hpText.gameObject.SetActive(false);
        }
        nameText.text = this.character.getName();
        this.character.OnHUDValuesChange += Refresh;
        this.character.OnHPValuesChange += HPFeedback;
        Refresh();
    }

    void HPFeedback(int pastHp, int amountChanged, bool wasCritic)
    {
        DamageFB damageFb = Instantiate(damageFbPrefab);
        damageFb.transform.SetParent(transform.parent, false);
        damageFb.getRectTransform().localPosition = transform.localPosition + Vector3.right * 50;
        damageFb.Initialize(amountChanged, wasCritic);
    }

    void SetHpBar(float v)
    {
        if (v >= 0 && v <= 1)
            hpSlider.value = v;
    }

    void SetHpNumbers(float hp, float maxHp)
    {
        hpText.text = hp + "/" + maxHp;
    }

    void Refresh()
    {
        nameText.text = this.character.getName();
        SetHpBar(character.getHp() / character.getMaxHp());
        SetHpNumbers(character.getHp(), character.getMaxHp());
    }

    void OnDisable()
    {
        if (character != null)
        {
            character.OnHUDValuesChange -= Refresh;
            character.OnHPValuesChange -= HPFeedback;
        }
    }
}
