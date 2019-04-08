using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{
    protected Attributes attributes;

    protected AnimatedSlider hpSlider;
    [SerializeField] protected DamageFB damageFbPrefab;
    [SerializeField] protected Image fillArea;

    static Color normalFillArea;
    static readonly Color poisonFillArea = new Color(0.54f, 0.24f, 0.9f);
    static readonly Color regenerationFillArea = new Color(0.9f, 0.48f, 0.24f);

    void Awake()
    {
        hpSlider = GetComponentInChildren<AnimatedSlider>();
        normalFillArea = fillArea.color;
    }

    public virtual void Initialize(Character character)
    {
        this.attributes = character.GetAttributes();

        this.attributes.OnHPValuesChange += HPFeedback;
        this.attributes.OnHUDValuesChange += Refresh;
        this.attributes.OnEffectsChange += EffectsChange;

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
        SetHpBar((float)attributes.GetHP() / (float)attributes.GetMaxHP());
    }

    protected void EffectsChange(Attributes.Attribute attribute, int duration)
    {
        if (attribute == Attributes.Attribute.HP)
        {
            if (duration > 0)
            {
                fillArea.color = regenerationFillArea;
                return;
            }
            if (duration == 0)
            {
                fillArea.color = normalFillArea;
                return;
            }
            if (duration < 0)
            {
                fillArea.color = poisonFillArea;
                return;
            }
        }
    }

    void OnEnable()
    {
        if (attributes != null)
        {
            attributes.OnHUDValuesChange += Refresh;
            attributes.OnHPValuesChange += HPFeedback;
            attributes.OnEffectsChange += EffectsChange;
        }
    }

    void OnDisable()
    {
        if (attributes != null)
        {
            attributes.OnHUDValuesChange -= Refresh;
            attributes.OnHPValuesChange -= HPFeedback;
            attributes.OnEffectsChange -= EffectsChange;
        }
    }
}
