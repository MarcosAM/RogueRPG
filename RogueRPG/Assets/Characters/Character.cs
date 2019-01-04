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
        //TODO Está Character HUD não tem uma referência a character então quando da Refresh ela não pega o nome novo
        attributes.GetHp().RefreshHUD();
    }

    public int GetRow() { return tile.GetRow(); }

    public virtual void ChangeEquipObject(Image backEquip, Image frontEquip)
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
            frontEquip.rectTransform.anchoredPosition = new Vector2(0, 0);
            frontEquip.rectTransform.localEulerAngles = Vector3.zero;
        }
        if (backEquip != null)
        {
            backEquip.rectTransform.SetParent(backHandler);
            backEquip.rectTransform.anchoredPosition = new Vector2(0, 0);
            backEquip.rectTransform.localEulerAngles = Vector3.zero;
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
}