using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    //TODO Delete Animator from Prefab
    [SerializeField] private Character combatant;
    [SerializeField] private TargetBtn targetButton;
    private RectTransform rectTransform;
    //[SerializeField] RectTransform portraitHandler;
    Tile tile;

    void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
        tile = GetComponent<Tile>();
    }

    public void Initialize(Tile tile)
    {
        this.combatant = null;

        //if (!tile.isFromHero())
        //{
        //    portraitHandler.localScale = new Vector3(-1, 1, 1);
        //}
        if (tile.getOccupant() != null)
        {
            combatant = tile.getOccupant();
            //combatant.transform.SetParent(portraitHandler);
            //combatant.transform.localPosition = new Vector3(0, -50);
        }
        else
        {
            this.combatant = null;
            if (targetButton != null)
                targetButton.Disappear();
        }
        targetButton.setTile(tile);
    }

    public void Deinitialize()
    {
        if (combatant != null)
        {
            //foreach (Transform transform in portraitHandler.transform)
            //{
            //    transform.SetParent(null);
            //}
            combatant = null;
        }
    }

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

    public Character getCharacter() { return combatant; }
}
