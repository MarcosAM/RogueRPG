using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour, IPlayAnimationByString
{
    public string Name { get; set; }

    protected Image avatarImg;
    protected Animator animator;
    IWaitForAnimationByString requester;

    Tile tile;

    Inventory inventory;
    Attributes attributes;

    public bool Playable { get; set; }

    public void Initialize()
    {
        inventory = GetComponent<Inventory>();
        attributes = GetComponent<Attributes>();
        attributes.Initialize(this);

        PrepareRectTransforms();

        animator = GetComponent<Animator>();
    }
    protected virtual void PrepareRectTransforms()
    {
        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();

        avatarImg = GetComponentInChildren<Image>();
    }

    public Image GetAvatarImg() { return avatarImg; }

    public int GetRow() { return tile.GetRow(); }

    public abstract void CreateEquipsSprites(Equip[] equips);
    public virtual void ChangeEquipObject(int equipIndex)
    {
        animator.SetTrigger("ChangeEquip");
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

    public virtual void RemoveSelf()
    {
        GetTile().SetC(null);
        Destroy(gameObject);
    }
}
