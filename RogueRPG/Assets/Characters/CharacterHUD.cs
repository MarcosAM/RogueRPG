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
        this.cName = character.GetName();

        nameText.text = this.cName;

        this.hp.OnHPValuesChange += HPFeedback;

        if (!character.Playable)
        {
            hpText.gameObject.SetActive(false);
            nameText.transform.rotation = Quaternion.Euler(0, 180, 90);
        }

        this.hp.OnHUDValuesChange += Refresh;

        Refresh();
    }

    void HPFeedback(int amountChanged, bool wasCritic)
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

    void SetHpNumbers(float hp, float maxHp)
    {
        hpText.text = hp.ToString();
    }

    void Refresh()
    {
        nameText.text = this.cName;
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

    public void SetCName(string name) { cName = name; }
}
