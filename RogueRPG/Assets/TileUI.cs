using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    //TODO Delete Animator from Prefab
    //[SerializeField] private Character combatant;
    [SerializeField] private TargetBtn targetButton;
    private RectTransform rectTransform;
    Tile tile;

    void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
        tile = GetComponent<Tile>();
        targetButton.setTile(tile);
    }

    //public void Initialize(Tile tile)
    //{
    //    this.combatant = null;
    //    if (tile.getOccupant() != null)
    //    {
    //        combatant = tile.getOccupant();
    //    }
    //    else
    //    {
    //        this.combatant = null;
    //        if (targetButton != null)
    //            targetButton.Disappear();
    //    }
    //    targetButton.setTile(tile);
    //}

    //public void Deinitialize()
    //{
    //    if (combatant != null)
    //    {
    //        combatant = null;
    //    }
    //}

    public void ShowTargetBtn(Character user, Equip skill)
    {
        if (tile.IsEnabled())
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

    public void CheckIfAffected(Tile target, Equip choosedSkill, Character user)
    {
        if (tile.IsEnabled())
            targetButton.CheckIfAffected(target, choosedSkill, user);
    }

    public Character getCharacter() { return tile.getOccupant(); }
}
