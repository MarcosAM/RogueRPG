using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{
    Hp hp;
    string cName;

    AnimatedSlider hpSlider;
    [SerializeField] Text hpText;
    [SerializeField] private DamageFB damageFbPrefab;
    [SerializeField] private Text nameText;

    void Awake()
    {
        hpSlider = GetComponentInChildren<AnimatedSlider>();
    }

    public void Initialize(Character character)
    {
        this.hp = character.GetAttributes().GetHp();
        this.name = character.GetName();

        if (character.IsPlayable())
        {
            hpText.gameObject.SetActive(false);
        }
        nameText.text = this.name;

        this.hp.OnHUDValuesChange += Refresh;
        this.hp.OnHPValuesChange += HPFeedback;

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
        nameText.text = this.name;
        SetHpBar(hp.GetValue() / hp.GetMaxValue());
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
