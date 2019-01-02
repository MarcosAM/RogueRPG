using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour, IPlayAnimationByString
{

    [SerializeField] protected string characterName;
    protected int hp;
    protected int maxHp;
    [SerializeField] protected bool alive = true;

    //TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
    [SerializeField] protected StandartStats stats;
    protected List<Stat> listStat = new List<Stat>();

    [SerializeField] protected Image avatarImg;
    [SerializeField] protected RectTransform frontHandler;
    [SerializeField] protected RectTransform backHandler;
    protected Animator animator;
    IWaitForAnimationByString requester;

    Tile tile;

    Momentum momentum;

    Inventory inventory;

    public event Action OnHUDValuesChange;
    public event Action<int, int, bool> OnHPValuesChange;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
        avatarImg = GetComponentInChildren<Image>();
        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();
        backHandler = transforms[0];
        frontHandler = transforms[2];
        animator = GetComponent<Animator>();
        BuffPManager buffPManager = FindObjectOfType<BuffPManager>();

        foreach (Stat.Stats stat in Enum.GetValues(typeof(Stat.Stats)))
        {
            listStat.Add(new Stat(this, stat, buffPManager));
        }
        if (stats != null)
        {
            FillStats();
        }
    }

    public void LoseHpBy(int damage, bool wasCritic)
    {

        if (damage > 0)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(hp, damage, wasCritic);
            }

            hp -= damage;
            animator.SetTrigger("Damage");

            if (hp <= 0)
            {
                Die();
            }
        }

        momentum.Value += IsPlayable() ? -(float)damage / 100 : (float)damage / 100;
        //TODO efeito de defender caso o dano seja menor que zero

        RefreshHUD();
    }

    public void Heal(int value)
    {
        if (value >= 0 && alive)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(hp, value, false);
            }
            hp += value;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
        RefreshHUD();
    }

    public void Die()
    {
        hp = 0;
        alive = false;
        EventManager.DeathOf(this);
        RemoveAllBuffs();
    }

    public void Revive(int hpRecovered)
    {
        alive = true;
        Heal(hpRecovered);
        DungeonManager.getInstance().AddToInitiative(this);
        RefreshHUD();
    }

    void RefreshHUD()
    {
        if (OnHUDValuesChange != null)
        {
            OnHUDValuesChange();
        }
    }

    protected virtual void FillStats()
    {
        inventory.SetEquips(this, stats.GetEquips());
        GetComponentInChildren<CharacterHUD>().SetCharacter(this);
        momentum = FindObjectOfType<Momentum>();
    }

    public void Refresh()
    {
        RemoveAllBuffs();
        hp = maxHp;
    }

    public void SpendBuffs()
    {
        foreach (Stat stat in listStat)
        {
            stat.SpendAndCheckIfEnded();
        }
    }

    void RemoveAllBuffs()
    {
        foreach (Stat stat in listStat)
        {
            stat.ResetBuff();
        }
    }

    public void BuffIt(Stat.Stats stats, Stat.Intensity intensity, int buffDuration)
    {
        listStat.Find(s => s.GetStats() == stats).BuffIt(intensity, buffDuration);
    }

    public void SetStats(StandartStats standartStats)
    {
        this.stats = standartStats;
        FillStats();
    }

    //public Equip[] GetEquips() { return equips; }

    public float GetHp() { return hp; }
    public float GetMaxHp() { return maxHp; }
    public string GetName() { return characterName; }

    public float GetStatValue(Stat.Stats stats)
    {
        if (listStat.Exists(s => s.GetStats() == stats))
        {
            return listStat.Find(s => s.GetStats() == stats).GetValue();
        }
        else
        {
            return 0;
        }
    }

    public Stat GetStat(Stat.Stats stats)
    {
        if (listStat.Exists(s => s.GetStats() == stats))
        {
            return listStat.Find(s => s.GetStats() == stats);
        }
        else
        {
            return null;
        }
    }

    public virtual bool IsPlayable() { return true; }
    public bool IsAlive() { return alive; }
    public Image GetAvatarImg() { return avatarImg; }

    public void SetName(string name)
    {
        this.characterName = name;
        RefreshHUD();
    }

    public int GetPosition()
    {
        return GetComponentInParent<Transform>().gameObject.GetComponentInParent<Tile>().GetRow();
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

    public Tile GetTile() { return tile; }
    public void SetTile(Tile tile) { this.tile = tile; }
    public List<Tile> GetEnemiesTiles() { return GetTile().GetEnemiesTiles(); }
    public List<Tile> GetAlliesTiles() { return GetTile().GetAlliesTiles(); }

    public bool IsBuffed(Stat.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).getBuffValue() > 0;
    }

    public float GetBuffValueOf(Stat.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).getBuffValue();
    }

    public Stat.Intensity GetBuffIntensity(Stat.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).GetIntensity();
    }

    public bool IsDebuffed()
    {
        return listStat.Exists(s => s.getBuffValue() < 0);
    }

    public bool IsDebuffed(Stat.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).getBuffValue() < 0;
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

    public Animator getAnimator()
    {
        return animator;
    }

    void finishedAnimationByString()
    {
        requester.ResumeFromAnimation(this);
    }

    public Momentum GetMomentum() { return momentum; }

    public void AddToMaxHp(int value)
    {
        this.maxHp += value;
        this.hp = this.maxHp;
    }
    public Inventory GetInventory() { return inventory; }
}