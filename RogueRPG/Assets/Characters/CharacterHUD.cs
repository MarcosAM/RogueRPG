using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHUD : MonoBehaviour
{
    protected Hp hp;

    protected AnimatedSlider hpSlider;
    [SerializeField] protected DamageFB damageFbPrefab;

    void Awake()
    {
        hpSlider = GetComponentInChildren<AnimatedSlider>();
    }

    public virtual void Initialize(Character character)
    {
        this.hp = character.GetAttributes().GetHp();

        this.hp.OnHPValuesChange += HPFeedback;
        this.hp.OnHUDValuesChange += Refresh;

        Refresh();
    }

    protected void HPFeedback(int amountChanged, bool wasCritic)
    {
        DamageFB damageFb = Instantiate(damageFbPrefab);
        damageFb.transform.SetParent(transform.parent.parent, false);
        damageFb.getRectTransform().localPosition = transform.localPosition + Vector3.right * 50;
        damageFb.Initialize(amountChanged, wasCritic);
    }

    void SetHpBar(float v)
    {
        if (v >= 0 && v <= 1)
            hpSlider.Value = v;
    }

    protected virtual void Refresh()
    {
        SetHpBar(hp.GetValue() / hp.GetMaxValue());
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
