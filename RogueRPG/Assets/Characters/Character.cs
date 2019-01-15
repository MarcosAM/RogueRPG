using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour, IPlayAnimationByString
{

    [SerializeField] protected string characterName;


    //TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
    [SerializeField] protected StandartStats stats;

    [SerializeField] protected Image avatarImg;
    [SerializeField] protected RectTransform frontHandler;
    [SerializeField] protected RectTransform backHandler;
    protected Animator animator;
    IWaitForAnimationByString requester;

    Tile tile;

    Inventory inventory;
    Attributes attributes;

    RectTransform[,] rectTransforms;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
        attributes = GetComponent<Attributes>();
        attributes.Initialize(this);

        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();

        avatarImg = GetComponentInChildren<Image>();
        backHandler = transforms[0];
        frontHandler = transforms[2];

        animator = GetComponent<Animator>();

        if (stats != null)
        {
            FillStats();
        }
    }

    protected virtual void FillStats()
    {
        inventory.SetEquips(this, stats.GetEquips());
        animator.runtimeAnimatorController = Archetypes.GetAnimator(inventory.Archetype);
        GetComponentInChildren<CharacterHUD>().Initialize(this);
    }

    public void SetStats(StandartStats standartStats)
    {
        this.stats = standartStats;
        FillStats();
    }

    public string GetName() { return characterName; }

    public virtual bool IsPlayable() { return true; }
    public Image GetAvatarImg() { return avatarImg; }

    public void SetName(string name)
    {
        this.characterName = name;
        GetComponentInChildren<CharacterHUD>().SetCName(name);
        attributes.GetHp().RefreshHUD();
    }

    public int GetRow() { return tile.GetRow(); }

    public void ChangeEquipObject(int equipIndex)
    {

        animator.SetTrigger("ChangeEquip");

        foreach (RectTransform child in frontHandler)
        {
            child.SetParent(null);
        }
        foreach (RectTransform child in backHandler)
        {
            child.SetParent(null);
        }

        if (rectTransforms[equipIndex, 0] != null)
        {
            rectTransforms[equipIndex, 0].SetParent(backHandler);
            rectTransforms[equipIndex, 0].anchoredPosition = new Vector2(0, 0);
            rectTransforms[equipIndex, 0].localEulerAngles = Vector3.zero;
        }
        if (rectTransforms[equipIndex, 1] != null)
        {
            rectTransforms[equipIndex, 1].SetParent(frontHandler);
            rectTransforms[equipIndex, 1].anchoredPosition = new Vector2(0, 0);
            rectTransforms[equipIndex, 1].localEulerAngles = Vector3.zero;
        }
    }

    public Tile GetTile() { return tile; }
    public void SetTile(Tile tile) { this.tile = tile; }
    public List<Tile> GetEnemiesTiles() { return GetTile().GetEnemiesTiles(); }
    public List<Tile> GetAlliesTiles() { return GetTile().GetAlliesTiles(); }

    public void UseSkillAnimation()
    {
        animator.SetTrigger("UseSkill");
    }

    public void PlayAnimation(IWaitForAnimationByString requester, string trigger)
    {
        animator.SetTrigger(trigger);
        this.requester = requester;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    void finishedAnimationByString()
    {
        requester.ResumeFromAnimation(this);
    }

    public Inventory GetInventory() { return inventory; }
    public Attributes GetAttributes() { return attributes; }

    public void CreateEquipsSprites(Equip[] equips)
    {
        if (rectTransforms != null)
        {
            for (var i = 0; i < equips.Length; i++)
            {
                Destroy(rectTransforms[i, 0].gameObject);
                Destroy(rectTransforms[i, 1].gameObject);
            }
        }

        rectTransforms = new RectTransform[equips.Length, 2];

        for (var i = 0; i < equips.Length; i++)
        {
            if (equips[i].GetBackEquipPrefab())
                rectTransforms[i, 0] = Instantiate(equips[i].GetBackEquipPrefab());
            else
                rectTransforms[i, 0] = null;
            if (equips[i].GetFrontEquipPrefab())
                rectTransforms[i, 1] = Instantiate(equips[i].GetFrontEquipPrefab());
            else
                rectTransforms[i, 1] = null;
        }
    }
}