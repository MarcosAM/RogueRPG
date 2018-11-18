using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{

    Character character;
    AnimatedSlider hpSlider;
    [SerializeField] Text hpText;
    [SerializeField] private DamageFB damageFbPrefab;
    [SerializeField] private Text nameText;

    void Awake()
    {
        hpSlider = GetComponentInChildren<AnimatedSlider>();
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
        if (!this.character.IsPlayable())
        {
            hpText.gameObject.SetActive(false);
        }
        nameText.text = this.character.GetName();
        //TODO Desconectar com outros Characters caso já tivesse um, o que é improvável
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
            hpSlider.Value = v;
    }

    void SetHpNumbers(float hp, float maxHp)
    {
        hpText.text = hp + "/" + maxHp;
    }

    void Refresh()
    {
        nameText.text = this.character.GetName();
        SetHpBar(character.GetHp() / character.GetMaxHp());
        SetHpNumbers(character.GetHp(), character.GetMaxHp());
    }

    void OnEnable()
    {
        if (character != null)
        {
            character.OnHUDValuesChange += Refresh;
            character.OnHPValuesChange += HPFeedback;
        }
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
