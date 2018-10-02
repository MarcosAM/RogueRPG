using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour, IComparable, IRegeneratable, IPoisonable
{

    [SerializeField] protected string characterName;
    protected int hp;
    protected int maxHp;
    protected float delayCountdown = 0;
    [SerializeField] protected bool alive = true;

    //TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
    [SerializeField] protected StandartStats stats;
    protected Stat atk, atkm, def, defm, precision, dodge, critic;
    protected CombatBehavior combatBehavior;
    [SerializeField] protected IMovable movement;

    public Equip[] equips = new Equip[4];
    protected Equip momentumSkill;

    [SerializeField] protected Image portrait;
    protected CombatantHUD hud;
    protected RegenerationAndPoisonManager regenerationManager;

    public event Action OnHUDValuesChange;
    public event Action<int, int, bool> OnHPValuesChange;
    public event Action OnMyTurnStarts;
    public event Action OnMyTurnEnds;
    public event Action OnBuffsGainOrLoss;

    void Awake()
    {
        atk = new Stat();
        atkm = new Stat();
        def = new Stat();
        defm = new Stat();
        critic = new Stat();
        precision = new Stat();
        dodge = new Stat();
        regenerationManager = new Character.RegenerationAndPoisonManager(this);
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
        //		RecoverFromDelayBy (delayCountdown*-1);
        SpendBuffs();
        hud.ShowItsActivePlayer();
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
        hud.TurnNameBackToBlack();
        EventManager.EndedTurn();
    }

    public void HitWith(Character target, float attack, Skill skill)
    {
        if (skill.GetSource() == Skill.Source.Physical)
        {
            if (skill.GetCritic() + critic.getValue() >= UnityEngine.Random.value)
                target.loseHpBy(Mathf.RoundToInt((attack + atk.getValue()) * 1.5f), true);
            else
                target.loseHpBy(Mathf.RoundToInt((attack + atk.getValue()) * UnityEngine.Random.Range(1f, 1.2f) - target.getDefValue()), false);
        }
        else
        {
            target.loseHpBy(Mathf.RoundToInt((attack + atkm.getValue()) * UnityEngine.Random.Range(1f, 1.2f) - target.getDefmValue()), false);
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

    public bool didIHitYou(float attackValue)
    {
        if (attackValue + getDodgeValue() < 0)
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
            if (Mathf.Abs(target.getIndex() - getPosition()) <= skillEffect.GetRange())
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
            return skill.GetPrecision() + precision.getValue() + getDistanceInfluenceOnPrecision(target, skill) - target.getDodgeValue();
        }
        else
        {
            return -1;
        }
    }

    public bool didIHitYouWith(float precision)
    {
        if (precision - getDodgeValue() >= 0)
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
                loseHpBy(Mathf.RoundToInt(damage - getDefValue()), false);
                return Mathf.RoundToInt(damage - getDefValue());
            }
        }
        else
        {
            loseHpBy(Mathf.RoundToInt(damage - getDefmValue()), false);
            return Mathf.RoundToInt(damage - getDefmValue());
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
        return getDistanceInfluenceOnPrecision(target.getIndex(), skill);
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
        this.atk.setStatBase(atk);
        this.atkm.setStatBase(atkm);
        this.def.setStatBase(def);
        this.defm.setStatBase(defm);
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
        atk.SpendAndCheckIfEnded();
        atkm.SpendAndCheckIfEnded();
        def.SpendAndCheckIfEnded();
        defm.SpendAndCheckIfEnded();
        critic.SpendAndCheckIfEnded();
        dodge.SpendAndCheckIfEnded();
        precision.SpendAndCheckIfEnded();
        if (OnBuffsGainOrLoss != null)
        {
            OnBuffsGainOrLoss();
        }
    }

    void RemoveAllBuffs()
    {
        atk.ResetBuff();
        atkm.ResetBuff();
        def.ResetBuff();
        defm.ResetBuff();
        dodge.ResetBuff();
        precision.ResetBuff();
        critic.ResetBuff();
        if (OnBuffsGainOrLoss != null)
        {
            OnBuffsGainOrLoss();
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

    public void AtkBuff(float buffValue, int buffDuration)
    {
        atk.BuffIt(buffValue, buffDuration);
        OnBuffsGainOrLoss();
    }
    public void AtkmBuff(float buffValue, int buffDuration)
    {
        atkm.BuffIt(buffValue, buffDuration);
        OnBuffsGainOrLoss();
    }
    public void DefBuff(float buffValue, int buffDuration)
    {
        def.BuffIt(buffValue, buffDuration);
        OnBuffsGainOrLoss();
    }
    public void DefmBuff(float buffValue, int buffDuration)
    {
        defm.BuffIt(buffValue, buffDuration);
        OnBuffsGainOrLoss();
    }
    public void CriticBuff(float buffValue, int buffDuration)
    {
        critic.BuffIt(buffValue, buffDuration);
        OnBuffsGainOrLoss();
    }
    public void DodgeBuff(float buffValue, int buffDuration)
    {
        dodge.BuffIt(buffValue, buffDuration);
        OnBuffsGainOrLoss();
    }
    public void PrecisionBuff(float buffValue, int buffDuration)
    {
        precision.BuffIt(buffValue, buffDuration);
        OnBuffsGainOrLoss();
    }

    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            return 1;
        }

        Character other = obj as Character;
        if (this.delayCountdown - other.getDelayCountdown() == 0)
        {
            return 0;
        }
        if (this.delayCountdown > other.getDelayCountdown())
        {
            return 1;
        }
        else
        {
            return -1;
        }
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

    public float getAtkValue() { return atk.getValue(); }
    public float getAtkmValue() { return atkm.getValue(); }
    public float getDefValue() { return def.getValue(); }
    public float getDefmValue() { return defm.getValue(); }
    public float getDodgeValue() { return dodge.getValue(); }
    public float getPrecisionValue() { return precision.getValue(); }

    public Stat getAtk() { return atk; }
    public Stat getDef() { return def; }
    public Stat getDefm() { return defm; }
    public Stat getAtkm() { return atkm; }
    public Stat getCritic() { return critic; }
    public Stat getPrecision() { return precision; }
    public Stat getDodge() { return dodge; }

    public virtual bool IsPlayable() { return true; }
    public bool isAlive() { return alive; }
    public Image getPortrait() { return portrait; }

    public float getDelayCountdown() { return delayCountdown; }

    public void setName(string name)
    {
        this.characterName = name;
    }
    public void setHUD(CombatantHUD combatantHUD) { hud = combatantHUD; }
    public CombatantHUD getHUD() { return hud; }
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
}