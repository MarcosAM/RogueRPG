using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour, IRegeneratable, IPoisonable
{

    [SerializeField] protected string characterName;
    protected int hp;
    protected int maxHp;
    protected float delayCountdown = 0;
    [SerializeField] protected bool alive = true;

    //TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
    [SerializeField] protected StandartStats stats;
    //protected Stat atk, atkm, def, defm, precision, dodge, critic;
    protected List<Stat> listaStat;
    protected CombatBehavior combatBehavior;
    [SerializeField] protected IMovable movement;

    public Equip[] equips = new Equip[5];
    protected Equip momentumSkill;

    [SerializeField] protected Image portrait;
    protected TileUI hud;
    protected RegenerationAndPoisonManager regenerationManager;

    public event Action OnHUDValuesChange;
    public event Action<int, int, bool> OnHPValuesChange;
    public event Action OnMyTurnStarts;
    public event Action OnMyTurnEnds;

    void Awake()
    {
        BuffPManager buffPManager = FindObjectOfType<BuffPManager>();

        foreach (Stat.Stats stat in Enum.GetValues(typeof(Stat.Stats)))
        {
            listaStat.Add(new Stat(this, stat, buffPManager));
        }

        //atk = new Stat(this, Stat.Stats.Atk, buffPManager);
        //atkm = new Stat(this, Stat.Stats.Atkm, buffPManager);
        //def = new Stat(this, Stat.Stats.Def, buffPManager);
        //defm = new Stat(this, Stat.Stats.Defm, buffPManager);
        //critic = new Stat(this, Stat.Stats.Critic, buffPManager);
        //precision = new Stat(this, Stat.Stats.Precision, buffPManager);
        //dodge = new Stat(this, Stat.Stats.Dodge, buffPManager);
        //regenerationManager = new Character.RegenerationAndPoisonManager(this);
        if (stats != null)
        {
            FillStats();
        }
        movement = GetComponent<IMovable>();
        movement.Initialize(this);
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

    public void TryToHitWith(Battleground.Tile target, Skill skillEffect)
    {
        if (getPrecisionOfSkillEffect(target, skillEffect) >= UnityEngine.Random.value)
        {
            skillEffect.OnHitEffect(this, target);
        }
        else
        {
            skillEffect.OnMissedEffect(this, target);
            if (target.getOccupant() != null)
                target.getOccupant().getHUD().getAnimator().SetTrigger("Dodge");
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
            hud.getAnimator().SetTrigger("Dodge");
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

    public bool CanIHitWith(Battleground.Tile target, Skill skillEffect)
    {
        //if (skillEffect.DoesTargetTile())
        //{
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
        //}
        //else
        //{
        //if (target.getOccupant() != null)
        //{
        //    return CanIHitWith(target.getOccupant(), skillEffect);
        //}
        //else
        //{
        //    return false;
        //}
        //}
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

    public float getPrecisionOfSkillEffect(Battleground.Tile target, Skill skill)
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
                //if (skill.DoesTargetTile())
                //{
                //    return skill.GetPrecision() + precision.getValue();
                //}
                //else
                //{
                //    return -1;
                //}
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

    public float getDistanceInfluenceOnPrecision(Battleground.Tile target, Skill skill)
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
            hud.getAnimator().SetTrigger("Damage");
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
        regenerationManager.poisened = false;
        regenerationManager.duration = 0;
    }

    public void revive(int hpRecovered)
    {
        alive = true;
        Heal(hpRecovered);
        DungeonManager.getInstance().addToInitiative(this);
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
    }

    public void refresh()
    {
        RemoveAllBuffs();
        delayCountdown = 0;
        hp = maxHp;
        regenerationManager.poisened = false;
        regenerationManager.duration = 0;
    }

    public void SpendBuffs()
    {
        foreach (Stat stat in listaStat)
        {
            stat.SpendAndCheckIfEnded();
        }
        //atk.SpendAndCheckIfEnded();
        //atkm.SpendAndCheckIfEnded();
        //def.SpendAndCheckIfEnded();
        //defm.SpendAndCheckIfEnded();
        //critic.SpendAndCheckIfEnded();
        //dodge.SpendAndCheckIfEnded();
        //precision.SpendAndCheckIfEnded();
    }

    void RemoveAllBuffs()
    {
        foreach (Stat stat in listaStat)
        {
            stat.ResetBuff();
        }
        //atk.ResetBuff();
        //atkm.ResetBuff();
        //def.ResetBuff();
        //defm.ResetBuff();
        //dodge.ResetBuff();
        //precision.ResetBuff();
        //critic.ResetBuff();
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

    //public void AtkBuff(Stat.Intensity intesity, int buffDuration)
    //{
    //    atk.BuffIt(intesity, buffDuration);
    //}
    //public void AtkmBuff(Stat.Intensity intensity, int buffDuration)
    //{
    //    atkm.BuffIt(intensity, buffDuration);
    //}
    //public void DefBuff(Stat.Intensity intensity, int buffDuration)
    //{
    //    def.BuffIt(intensity, buffDuration);
    //}
    //public void DefmBuff(Stat.Intensity intensity, int buffDuration)
    //{
    //    defm.BuffIt(intensity, buffDuration);
    //}
    //public void CriticBuff(Stat.Intensity intensity, int buffDuration)
    //{
    //    critic.BuffIt(intensity, buffDuration);
    //}
    //public void DodgeBuff(Stat.Intensity intensity, int buffDuration)
    //{
    //    dodge.BuffIt(intensity, buffDuration);
    //}
    //public void PrecisionBuff(Stat.Intensity intensity, int buffDuration)
    //{
    //    precision.BuffIt(intensity, buffDuration);
    //}

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

    //public float getAtkValue() { return atk.GetValue(); }
    //public float getAtkmValue() { return atkm.GetValue(); }
    //public float getDefValue() { return def.GetValue(); }
    //public float getDefmValue() { return defm.GetValue(); }
    //public float getDodgeValue() { return dodge.GetValue(); }
    //public float getPrecisionValue() { return precision.GetValue(); }

    //public Stat getAtk() { return atk; }
    //public Stat getDef() { return def; }
    //public Stat getDefm() { return defm; }
    //public Stat getAtkm() { return atkm; }
    //public Stat getCritic() { return critic; }
    //public Stat getPrecision() { return precision; }
    //public Stat getDodge() { return dodge; }

    public virtual bool IsPlayable() { return true; }
    public bool isAlive() { return alive; }
    public Image getPortrait() { return portrait; }

    public float getDelayCountdown() { return delayCountdown; }

    public void setName(string name)
    {
        this.characterName = name;
    }
    public void setHUD(TileUI combatantHUD) { hud = combatantHUD; }
    public TileUI getHUD() { return hud; }
    public CombatBehavior getBehavior() { return combatBehavior; }
    public IMovable getMovement() { return movement; }
    public int getPosition() { return movement.getPosition(); }

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
                print("WORK!");
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
        this.hud.changeEquipObject(backEquip, frontEquip);
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

    public void PlayAnimation(IWaitForAnimationByString requester, string trigger)
    {
        hud.PlayAnimation(requester, trigger);
    }

    public Battleground.Tile GetTile() { return movement.GetTile(); }
    public Battleground.Tile[] GetEnemiesTiles() { return GetTile().GetEnemiesTiles(); }
    public Battleground.Tile[] GetAlliesTiles() { return GetTile().GetAlliesTiles(); }

    public bool IsMomentumEquip(Equip equip)
    {
        return equip == equips[equips.Length - 1];
    }

    public bool IsBuffed(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).getBuffValue() > 0;
        //switch (stats)
        //{
        //    case Stat.Stats.Critic:
        //        if (critic.getBuffValue() > 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Dodge:
        //        if (dodge.getBuffValue() > 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Precision:
        //        if (precision.getBuffValue() > 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Atk:
        //        if (atk.getBuffValue() > 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Atkm:
        //        if (atkm.getBuffValue() > 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Def:
        //        if (def.getBuffValue() > 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Defm:
        //        if (defm.getBuffValue() > 0)
        //            return true;
        //        else
        //            return false;
        //    default:
        //        return false;
        //}
    }

    public float GetBuffValueOf(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).getBuffValue();
        //switch (stats)
        //{
        //    case Stat.Stats.Critic:
        //        return critic.getBuffValue();
        //    case Stat.Stats.Dodge:
        //        return dodge.getBuffValue();
        //    case Stat.Stats.Precision:
        //        return precision.getBuffValue();
        //    case Stat.Stats.Atk:
        //        return atk.getBuffValue();
        //    case Stat.Stats.Atkm:
        //        return atkm.getBuffValue();
        //    case Stat.Stats.Def:
        //        return def.getBuffValue();
        //    case Stat.Stats.Defm:
        //        return defm.getBuffValue();
        //    default:
        //        return 0;
        //}
    }

    public Stat.Intensity GetBuffIntensity(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).GetIntensity();
        //switch (stats)
        //{
        //    case Stat.Stats.Critic:
        //        return critic.GetIntensity();
        //    case Stat.Stats.Dodge:
        //        return dodge.GetIntensity();
        //    case Stat.Stats.Precision:
        //        return precision.GetIntensity();
        //    case Stat.Stats.Atk:
        //        return atk.GetIntensity();
        //    case Stat.Stats.Atkm:
        //        return atkm.GetIntensity();
        //    case Stat.Stats.Def:
        //        return def.GetIntensity();
        //    case Stat.Stats.Defm:
        //        return defm.GetIntensity();
        //    default:
        //        return Stat.Intensity.None;
        //}
    }

    public bool IsDebuffed()
    {
        //if (atk.getBuffValue() < 0)
        //    return true;
        //if (atkm.getBuffValue() < 0)
        //    return true;
        //if (def.getBuffValue() < 0)
        //    return true;
        //if (defm.getBuffValue() < 0)
        //    return true;
        //if (critic.getBuffValue() < 0)
        //    return true;
        //if (precision.getBuffValue() < 0)
        //    return true;
        //if (dodge.getBuffValue() < 0)
        //    return true;
        //return false;
        return listaStat.Exists(s => s.getBuffValue() < 0);
    }

    public bool IsDebuffed(Stat.Stats stats)
    {
        return listaStat.Find(s => s.GetStats() == stats).getBuffValue() < 0;
        //switch (stats)
        //{
        //    case Stat.Stats.Critic:
        //        if (critic.getBuffValue() < 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Dodge:
        //        if (dodge.getBuffValue() < 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Precision:
        //        if (precision.getBuffValue() < 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Atk:
        //        if (atk.getBuffValue() < 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Atkm:
        //        if (atkm.getBuffValue() < 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Def:
        //        if (def.getBuffValue() < 0)
        //            return true;
        //        else
        //            return false;
        //    case Stat.Stats.Defm:
        //        if (defm.getBuffValue() < 0)
        //            return true;
        //        else
        //            return false;
        //    default:
        //        return false;
        //}
    }
}