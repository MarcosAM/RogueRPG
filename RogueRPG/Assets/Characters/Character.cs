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
    protected List<Stat> listaStat = new List<Stat>();
    protected CombatBehavior combatBehavior;
    protected int level = 0;

    public Archetypes.Archetype Archetype { get; set; }

    public Equip[] equips = new Equip[5];
    //protected Equip momentumSkill;

    [SerializeField] protected Image avatarImg;
    [SerializeField] protected RectTransform frontHandler;
    [SerializeField] protected RectTransform backHandler;
    protected Animator animator;
    IWaitForAnimationByString requester;

    Tile tile;

    public event Action OnHUDValuesChange;
    public event Action<int, int, bool> OnHPValuesChange;
    public event Action OnMyTurnStarts;
    public event Action OnMyTurnEnds;

    void Awake()
    {
        avatarImg = GetComponentInChildren<Image>();
        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();
        backHandler = transforms[0];
        frontHandler = transforms[2];
        animator = GetComponent<Animator>();
        BuffPManager buffPManager = FindObjectOfType<BuffPManager>();

        foreach (Stat.Stats stat in Enum.GetValues(typeof(Stat.Stats)))
        {
            listaStat.Add(new Stat(this, stat, buffPManager));
        }
        if (stats != null)
        {
            FillStats();
        }
    }

    public void StartTurn()
    {
        if (OnMyTurnStarts != null)
        {
            OnMyTurnStarts();
        }
        SpendBuffs();
        CheckIfSkillsShouldBeRefreshed();
    }

    public void EndTurn()
    {
        if (OnMyTurnEnds != null)
        {
            OnMyTurnEnds();
        }
        EventManager.EndedTurn();
    }

    public bool DidIHitYouWith(float precision)
    {
        if (precision - GetStatValue(Stat.Stats.Dodge) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //public int TakeDamage(int damage, Skill.Source damageSource, bool wasCritic)
    //{
    //    if (damageSource == Skill.Source.Physical)
    //    {
    //        if (wasCritic)
    //        {
    //            LoseHpBy(Mathf.RoundToInt(damage), true);
    //            return Mathf.RoundToInt(damage);
    //        }
    //        else
    //        {
    //            LoseHpBy(Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Def)), false);
    //            return Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Def));
    //        }
    //    }
    //    else
    //    {
    //        LoseHpBy(Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Defm)), false);
    //        return Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Defm));
    //    }
    //}

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
        equips = stats.GetEquips();
        combatBehavior = GetComponent<CombatBehavior>();
        combatBehavior.SetCharacter(this);
        int hp = 0;
        int atk = 0;
        int atkm = 0;
        int def = 0;
        int defm = 0;
        foreach (Equip skill in equips)
        {
            hp += skill.GetHp();
            atk += skill.GetAtk();
            atkm += skill.GetAtkm();
            def += skill.GetDef();
            defm += skill.GetDefm();
        }
        this.GetStat(Stat.Stats.Atk).setStatBase(atk);
        this.GetStat(Stat.Stats.Atkm).setStatBase(atkm);
        this.GetStat(Stat.Stats.Def).setStatBase(def);
        this.GetStat(Stat.Stats.Defm).setStatBase(defm);
        this.maxHp = hp;
        this.hp = maxHp;
        GetComponentInChildren<CharacterHUD>().SetCharacter(this);
        Archetype = Archetypes.GetArchetype(equips);
        foreach (var equip in equips)
        {
            level += equip.GetLevel();
        }
        equips[equips.Length - 1] = Archetypes.GetMomentumEquip(Archetype, level);
    }

    public void Refresh()
    {
        RemoveAllBuffs();
        hp = maxHp;
    }

    public void SpendBuffs()
    {
        foreach (Stat stat in listaStat)
        {
            stat.SpendAndCheckIfEnded();
        }
    }

    void RemoveAllBuffs()
    {
        foreach (Stat stat in listaStat)
        {
            stat.ResetBuff();
        }
    }

    void CheckIfSkillsShouldBeRefreshed()
    {
        if (combatBehavior.AtLeastOneEquipAvailable())
        {
            return;
        }
        else
        {
            combatBehavior.SetEquipsAvailability(true);
        }
    }

    public void BuffIt(Stat.Stats stats, Stat.Intensity intensity, int buffDuration)
    {
        listaStat.Find(s => s.GetStats() == stats).BuffIt(intensity, buffDuration);
    }

    public void SetStats(StandartStats standartStats)
    {
        this.stats = standartStats;
        FillStats();
    }

    public Equip[] GetEquips() { return equips; }
    //public Equip GetMomentumSkill() { return momentumSkill; }
    public List<Equip> GetUsableEquips()
    {
        List<Equip> usableSkills = new List<Equip>();
        for (int i = 0; i < equips.Length; i++)
        {
            if (combatBehavior.IsEquipAvailable(i))
            {
                usableSkills.Add(equips[i]);
            }
        }
        return usableSkills;
    }
    public float GetHp() { return hp; }
    public float GetMaxHp() { return maxHp; }
    public string GetName() { return characterName; }

    public float GetStatValue(Stat.Stats stats)
    {
        if (listaStat.Exists(s => s.GetStats() == stats))
        {
            return listaStat.Find(s => s.GetStats() == stats).GetValue();
        }
        else
        {
            return 0;
        }
    }

    public Stat GetStat(Stat.Stats stats)
    {
        if (listaStat.Exists(s => s.GetStats() == stats))
        {
            return listaStat.Find(s => s.GetStats() == stats);
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

    public CombatBehavior GetBehavior() { return combatBehavior; }

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

    public int howManyOf(Equip equip)
    {
        int c = 0;
        for (int i = 0; i < equips.Length; i++)
        {
            if (equip == equips[i])
            {
                c++;
            }
        }
        return c;
    }

    public Tile GetTile() { return tile; }
    public void SetTile(Tile tile) { this.tile = tile; }
    public List<Tile> GetEnemiesTiles() { return GetTile().GetEnemiesTiles(); }
    public List<Tile> GetAlliesTiles() { return GetTile().GetAlliesTiles(); }

    public bool IsMomentumEquip(Equip equip)
    {
        return equip == equips[equips.Length - 1];
    }

    public bool IsBuffed(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).getBuffValue() > 0;
    }

    public float GetBuffValueOf(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).getBuffValue();
    }

    public Stat.Intensity GetBuffIntensity(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).GetIntensity();
    }

    public bool IsDebuffed()
    {
        return listaStat.Exists(s => s.getBuffValue() < 0);
    }

    public bool IsDebuffed(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).getBuffValue() < 0;
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

    public float GetSumOfStats()
    {
        float f = 0;
        foreach (Stat stat in listaStat)
        {
            f += stat.GetValue();
        }
        return f;
    }
}