using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour, IRegeneratable, IPoisonable, IPlayAnimationByString
{

    [SerializeField] protected string characterName;
    protected int hp;
    protected int maxHp;
    protected float delayCountdown = 0;
    [SerializeField] protected bool alive = true;

    //TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
    [SerializeField] protected StandartStats stats;
    protected List<Stat> listaStat = new List<Stat>();
    protected CombatBehavior combatBehavior;
    //[SerializeField] protected CharacterMovement movement;

    public Equip[] equips = new Equip[5];
    protected Equip momentumSkill;

    //[SerializeField] protected Image portrait;
    //protected TileUI hud;
    protected RegenerationAndPoisonManager regenerationManager;

    [SerializeField] protected Image avatarImg;
    [SerializeField] protected RectTransform frontHandler;
    [SerializeField] protected RectTransform backHandler;
    protected Animator animator;
    IWaitForAnimationByString requester;

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
        //movement = GetComponent<CharacterMovement>();
        //movement.Initialize(this);
    }

    public void StartTurn()
    {
        if (OnMyTurnStarts != null)
        {
            OnMyTurnStarts();
        }
        SpendBuffs();
        CheckIfSkillsShouldBeRefreshed();
        if (regenerationManager != null)
        {
            regenerationManager.recover();
        }
        else
        {

        }
    }

    public void EndTurn()
    {
        if (OnMyTurnEnds != null)
        {
            OnMyTurnEnds();
        }
        EventManager.EndedTurn();
    }

    public void HitWith(Character target, float attack, Skill skill)
    {
        if (skill.GetSource() == Skill.Source.Physical)
        {
            if (skill.GetCritic() + GetStatValue(Stat.Stats.Critic) >= UnityEngine.Random.value)
                target.loseHpBy(Mathf.RoundToInt((attack + GetStatValue(Stat.Stats.Atk)) * 1.5f), true);
            else
                target.loseHpBy(Mathf.RoundToInt((attack + GetStatValue(Stat.Stats.Atk)) * UnityEngine.Random.Range(1f, 1.2f) - target.GetStatValue(Stat.Stats.Def)), false);
        }
        else
        {
            target.loseHpBy(Mathf.RoundToInt((attack + GetStatValue(Stat.Stats.Atkm)) * UnityEngine.Random.Range(1f, 1.2f) - target.GetStatValue(Stat.Stats.Defm)), false);
        }
    }

    public void TryToHitWith(Tile target, Skill skillEffect)
    {
        if (getPrecisionOfSkillEffect(target, skillEffect) >= UnityEngine.Random.value)
        {
            skillEffect.OnHitEffect(this, target);
        }
        else
        {
            skillEffect.OnMissedEffect(this, target);
            if (target.getOccupant() != null)
                //target.getOccupant().getHUD().getAnimator().SetTrigger("Dodge");
                target.getOccupant().getAnimator().SetTrigger("Dodge");
        }
    }

    public bool DidIHitYou(float attackValue)
    {
        if (attackValue + GetStatValue(Stat.Stats.Dodge) < 0)
        {
            return true;
        }
        else
        {
            //hud.getAnimator().SetTrigger("Dodge");
            animator.SetTrigger("Dodge");
            return false;
        }
    }

    public bool CanIHitWith(Character target, Skill skillEffect)
    {
        if (skillEffect.GetSkillType() == Skill.Type.Ranged)
        {
            return true;
        }
        else
        {
            if (Mathf.Abs(target.getPosition() - getPosition()) <= skillEffect.GetRange())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool CanIHitWith(Tile target, Skill skillEffect)
    {
        if (skillEffect.GetSkillType() == Skill.Type.Ranged)
        {
            return true;
        }
        else
        {
            if (Mathf.Abs(target.GetRow() - getPosition()) <= skillEffect.GetRange())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public float getPrecisionOfSkillEffect(Character target, Skill skill)
    {
        if (CanIHitWith(target, skill))
        {
            return skill.GetPrecision() + GetStatValue(Stat.Stats.Precision) + getDistanceInfluenceOnPrecision(target, skill) - target.GetStatValue(Stat.Stats.Dodge);
        }
        else
        {
            return -1;
        }
    }

    public bool didIHitYouWith(float precision)
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

    public int takeDamage(int damage, Skill.Source damageSource, bool wasCritic)
    {
        if (damageSource == Skill.Source.Physical)
        {
            if (wasCritic)
            {
                loseHpBy(Mathf.RoundToInt(damage), true);
                return Mathf.RoundToInt(damage);
            }
            else
            {
                loseHpBy(Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Def)), false);
                return Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Def));
            }
        }
        else
        {
            loseHpBy(Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Defm)), false);
            return Mathf.RoundToInt(damage - GetStatValue(Stat.Stats.Defm));
        }
    }

    public float getPrecisionOfSkillEffect(Tile target, Skill skill)
    {
        if (CanIHitWith(target, skill))
        {
            if (target.getOccupant() != null)
            {
                return getPrecisionOfSkillEffect(target.getOccupant(), skill);
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return -1;
        }
    }

    public float getDistanceInfluenceOnPrecision(Character target, Skill skill)
    {
        return getDistanceInfluenceOnPrecision(target.getPosition(), skill);
    }

    public float getDistanceInfluenceOnPrecision(Tile target, Skill skill)
    {
        return getDistanceInfluenceOnPrecision(target.GetRow(), skill);
    }

    public float getDistanceInfluenceOnPrecision(int targetPosition, Skill skill)
    {
        if (skill.GetSkillType() == Skill.Type.Melee)
        {
            return 0f;
        }
        else
        {
            float distanceInfluenceOnPrecision = skill.GetRange() - Mathf.Abs(getPosition() - targetPosition) * 0.1f;
            if (distanceInfluenceOnPrecision > 0)
            {
                return 0;
            }
            else
            {
                return distanceInfluenceOnPrecision;
            }
        }
    }

    public void loseHpBy(int damage, bool wasCritic)
    {
        if (damage > 0)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(hp, damage, wasCritic);
            }
            hp -= damage;
            //hud.getAnimator().SetTrigger("Damage");
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
        delayCountdown = 0;
        alive = false;
        EventManager.DeathOf(this);
        RemoveAllBuffs();
        //regenerationManager.poisened = false;
        //regenerationManager.duration = 0;
    }

    public void revive(int hpRecovered)
    {
        alive = true;
        Heal(hpRecovered);
        DungeonManager.getInstance().AddToInitiative(this);
        RefreshHUD();
    }

    public void DelayBy(float amount)
    {
        delayCountdown -= amount;
        RefreshHUD();
    }

    public void RecoverFromDelayBy(float amount)
    {
        if (alive)
        {
            delayCountdown += amount;
            RefreshHUD();
        }
    }
    public bool IsDelayed()
    {
        if (delayCountdown >= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
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
        equips = stats.getSkills();
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
    }

    public void refresh()
    {
        RemoveAllBuffs();
        delayCountdown = 0;
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

    public void setStats(StandartStats standartStats)
    {
        this.stats = standartStats;
        FillStats();
    }

    public Equip[] getEquips() { return equips; }
    public Equip getMomentumSkill() { return momentumSkill; }
    public List<Equip> getUsableEquips()
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
    public float getHp() { return hp; }
    public float getEnergy() { return delayCountdown; }
    public float getMaxHp() { return maxHp; }
    public string getName() { return characterName; }

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
    public bool isAlive() { return alive; }
    public Image GetAvatarImg() { return avatarImg; }

    public float getDelayCountdown() { return delayCountdown; }

    public void setName(string name)
    {
        this.characterName = name;
        RefreshHUD();
    }

    public CombatBehavior getBehavior() { return combatBehavior; }

    public int getPosition()
    {
        return GetComponentInParent<Transform>().gameObject.GetComponentInParent<Tile>().GetRow();
    }

    protected class RegenerationAndPoisonManager
    {
        public int duration = 0;
        public bool consumable = true;
        public bool poisened = false;
        Character owner;

        public void recover()
        {
            if (poisened)
            {
                owner.loseHpBy(Mathf.RoundToInt(owner.getMaxHp() * 0.1f), false);
            }
            else
            {
                if (duration > 0 || !consumable)
                {
                    owner.Heal(Mathf.RoundToInt(owner.getMaxHp() * 0.1f));
                    if (consumable)
                    {
                        duration--;
                    }
                }
            }
        }

        public RegenerationAndPoisonManager(Character owner)
        {
            this.owner = owner;
        }
    }

    public void startGeneration(int duration)
    {
        regenerationManager.duration += duration;
        regenerationManager.poisened = false;
    }
    public void startGeneration()
    {
        regenerationManager.consumable = false;
        regenerationManager.poisened = false;
    }
    public void getPoisoned()
    {
        regenerationManager.duration = 0;
        regenerationManager.poisened = true;
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

    public Tile GetTile()
    {
        return GetComponentInParent<Transform>().gameObject.GetComponentInParent<Tile>();
        //return movement.GetTile();
    }
    public Tile[] GetEnemiesTiles() { return GetTile().GetEnemiesTiles(); }
    public Tile[] GetAlliesTiles() { return GetTile().GetAlliesTiles(); }

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
}