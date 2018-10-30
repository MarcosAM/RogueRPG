using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour, IPlayAnimationByString
{

    [SerializeField] private Character combatant;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Text hpNumbers;
    [SerializeField] private TargetBtn targetButton;
    [SerializeField] private Image image;
    [SerializeField] private Text buffText;
    [SerializeField] private DamageFB damageFbPrefab;
    [SerializeField] private Text initiative;
    [SerializeField] private Text name;
    private RectTransform rectTransform;
    [SerializeField] RectTransform portraitHandler;
    [SerializeField] RectTransform frontHandler;
    [SerializeField] RectTransform backHandler;
    [SerializeField] private Image equipment;
    Animator animator;
    IWaitForAnimationByString requester;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(Battleground.Tile tile)
    {
        if (tile.isFromHero())
        {
            hpNumbers.gameObject.SetActive(true);
        }
        else
        {
            portraitHandler.localScale = new Vector3(-1, 1, 1);
            hpNumbers.gameObject.SetActive(false);
        }
        if (tile.getOccupant() != null)
        {
            combatant = tile.getOccupant();
            image.gameObject.SetActive(true);
            image.sprite = combatant.getPortrait().sprite;
            hpBar.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
            name.text = combatant.getName();
            combatant.OnHUDValuesChange += Refresh;
            combatant.OnHPValuesChange += HPFeedback;
            Refresh();
        }
        else
        {
            name.gameObject.SetActive(false);
            this.combatant = null;
            image.sprite = null;
            image.gameObject.SetActive(false);
            hpBar.gameObject.SetActive(false);
            hpNumbers.gameObject.SetActive(false);
            if (targetButton != null)
                targetButton.Disappear();
        }
        targetButton.setTile(tile);
    }

    public void Deinitialize()
    {
        if (combatant != null)
        {
            combatant.OnHUDValuesChange -= Refresh;
            combatant.OnHPValuesChange -= HPFeedback;
            combatant = null;
        }
    }

    public void HPFeedback(int pastHp, int amountChanged, bool wasCritic)
    {
        DamageFB damageFb = Instantiate(damageFbPrefab);
        damageFb.transform.SetParent(transform.parent, false);
        damageFb.getRectTransform().localPosition = rectTransform.localPosition + Vector3.right * 50;
        damageFb.Initialize(amountChanged, wasCritic);
    }

    public void setHpBar(float v)
    {
        if (v >= 0 && v <= 1)
            hpBar.value = v;
    }

    public void setHpNumbers(float hp, float maxHp)
    {
        hpNumbers.text = hp + "/" + maxHp;
    }

    public void Refresh()
    {
        setHpBar(combatant.getHp() / combatant.getMaxHp());
        setHpNumbers(combatant.getHp(), combatant.getMaxHp());
    }

    public void UseSkillAnimation()
    {
        animator.SetTrigger("UseSkill");
    }

    public void PlayAnimation(IWaitForAnimationByString requester, string trigger)
    {
        animator.SetTrigger(trigger);
        this.requester = requester;
    }

    void finishedAnimationByString()
    {
        requester.ResumeFromAnimation(this);
    }

    public void ShowTargetBtn(Character user, Equip skill)
    {
        targetButton.Appear(user, skill);
    }

    public void HideTargetBtn()
    {
        targetButton.Disappear();
    }

    public RectTransform getRectTransform()
    {
        return rectTransform;
    }
    public Animator getAnimator()
    {
        return animator;
    }

    public void ShowPossibleInitiative(Character activeCharacter, Equip skill)
    {
        DungeonManager dungeonManager = DungeonManager.getInstance();

    }

    public void changeEquipObject(Image backEquip, Image frontEquip)
    {
        foreach (RectTransform child in frontHandler)
        {
            child.SetParent(null);
        }
        foreach (RectTransform child in backHandler)
        {
            child.SetParent(null);
        }
        if (frontEquip != null)
        {
            frontEquip.rectTransform.SetParent(frontHandler);
            frontEquip.rectTransform.anchoredPosition = new Vector2(0f, 100f);
            frontEquip.rectTransform.localEulerAngles = Vector3.zero;
        }
        if (backEquip != null)
        {
            backEquip.rectTransform.SetParent(backHandler);
            backEquip.rectTransform.anchoredPosition = new Vector2(0f, 100f);
            backEquip.rectTransform.localEulerAngles = Vector3.zero;
        }
    }

    public void showTargetBtnWithColor(Color color)
    {
        targetButton.Show(color);
    }

    public void CheckIfAffected(Battleground.Tile target, Equip choosedSkill, Character user)
    {
        targetButton.CheckIfAffected(target, choosedSkill, user);
    }

    void OnDisable()
    {
        if (combatant != null)
        {
            combatant.OnHUDValuesChange -= Refresh;
        }
    }

    public Character getCharacter() { return combatant; }
}
