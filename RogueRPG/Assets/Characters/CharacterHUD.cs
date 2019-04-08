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
    [SerializeField] protected Image atkH, atkV, defH, defV, agiH, agiV;

    static Color normalFillArea;
    static readonly Color poisonFillArea = new Color(0.54f, 0.24f, 0.9f);
    static readonly Color regenerationFillArea = new Color(0.9f, 0.48f, 0.24f);

    void Awake()
    {
        hpSlider = GetComponentInChildren<AnimatedSlider>();
        normalFillArea = fillArea.color;

        SetAtkVisibility(false);
        SetDefVisibility(false);
        SetAgiVisibility(false);
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

        if (attribute == Attributes.Attribute.ATK)
        {
            if (duration >= 0)
            {
                SetAtkVisibility(duration > 0);
            }
            else
            {
                AtkDebuff();
            }
            return;
        }

        if (attribute == Attributes.Attribute.DEF)
        {
            if (duration >= 0)
            {
                SetDefVisibility(duration > 0);
            }
            else
            {
                DefDebuff();
            }
            return;
        }

        if (attribute == Attributes.Attribute.AGI)
        {
            if (duration >= 0)
            {
                SetAgiVisibility(duration > 0);
            }
            else
            {
                AgiDebuff();
            }
            return;
        }
    }

    void SetAtkVisibility(bool visibility)
    {
        atkH.gameObject.SetActive(visibility);
        atkV.gameObject.SetActive(visibility);
    }

    void AtkDebuff()
    {
        atkH.gameObject.SetActive(true);
        atkV.gameObject.SetActive(false);
    }

    void SetDefVisibility(bool visibility)
    {
        defH.gameObject.SetActive(visibility);
        defV.gameObject.SetActive(visibility);
    }

    void DefDebuff()
    {
        defH.gameObject.SetActive(true);
        defV.gameObject.SetActive(false);
    }

    void SetAgiVisibility(bool visibility)
    {
        agiH.gameObject.SetActive(visibility);
        agiV.gameObject.SetActive(visibility);
    }

    void AgiDebuff()
    {
        agiH.gameObject.SetActive(true);
        agiV.gameObject.SetActive(false);
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
