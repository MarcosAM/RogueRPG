using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombHUDManager : MonoBehaviour
{
    [SerializeField] TileUI[] heroesCombatantHUD = new TileUI[4];
    [SerializeField] TileUI[] enemiesCombatantHUD = new TileUI[4];
    [SerializeField] List<TileUI> tileUIs = new List<TileUI>();

    static CombHUDManager instance = null;

    public void Awake()
    {
        MakeItASingleton();
    }

    public void ShowCombatants(List<Battleground.Tile> tiles)
    {
        //foreach (TileUI tileUI in tileUIs)
        //{
        //    tileUI.Deinitialize();
        //    tileUI.gameObject.SetActive(false);
        //}

        //for (int i = 0; i < tiles.Count; i++)
        //{
        //    tileUIs[i].gameObject.SetActive(true);
        //    tileUIs[i].Initialize(tiles[i]);
        //    if (tiles[i].getOccupant() != null)
        //        tiles[i].getOccupant().setHUD(tileUIs[i]);
        //}

        ClearCombatantsHUDs();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (i < tiles.Count / 2)
            {
                enemiesCombatantHUD[i].gameObject.SetActive(true);
                enemiesCombatantHUD[i].Initialize(tiles[i]);
                if (tiles[i].getOccupant() != null)
                    tiles[i].getOccupant().setHUD(enemiesCombatantHUD[i]);
            }
            else
            {
                heroesCombatantHUD[i - tiles.Count / 2].gameObject.SetActive(true);
                heroesCombatantHUD[i - tiles.Count / 2].Initialize(tiles[i]);
                if (tiles[i].getOccupant() != null)
                    tiles[i].getOccupant().setHUD(heroesCombatantHUD[i - tiles.Count / 2]);
            }
        }
    }

    public void ClearCombatantsHUDs()
    {
        foreach (TileUI combatantHUD in heroesCombatantHUD)
        {
            combatantHUD.Deinitialize();
            combatantHUD.gameObject.SetActive(false);
        }

        foreach (TileUI combatantHUD in enemiesCombatantHUD)
        {
            combatantHUD.Deinitialize();
            combatantHUD.gameObject.SetActive(false);
        }
    }

    void MakeItASingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefreshInitiativeHUD()
    {
        foreach (TileUI hud in heroesCombatantHUD)
        {
            hud.RefreshInitiative();
        }
        foreach (TileUI hud in enemiesCombatantHUD)
        {
            hud.RefreshInitiative();
        }
    }

    public void ShowTargetBtns(Character user, Equip choosedSkill)
    {
        for (int i = 0; i < heroesCombatantHUD.Length; i++)
        {
            heroesCombatantHUD[i].ShowTargetBtn(user, choosedSkill);
        }
        for (int i = 0; i < enemiesCombatantHUD.Length; i++)
        {
            enemiesCombatantHUD[i].ShowTargetBtn(user, choosedSkill);
        }
    }

    public void HideTargetBtns(bool asPreview)
    {
        for (int i = 0; i < heroesCombatantHUD.Length; i++)
        {
            heroesCombatantHUD[i].HideTargetBtn();
        }
        for (int i = 0; i < enemiesCombatantHUD.Length; i++)
        {
            enemiesCombatantHUD[i].HideTargetBtn();
        }
    }


    public void onTargetBtnPressed(Battleground.Tile targetTile)
    {
        FindObjectOfType<PlayerInputManager>().ReturnEquipAndTarget(targetTile);
    }

    public void OnTargetBtnHoverEnter(TargetBtn targetBtn)
    {
        FindObjectOfType<PlayerInputManager>().HoverTargetBtnEnter(targetBtn);
    }

    public void PreviewTargets(Character user, Equip selectedEquip, Battleground.Tile target)
    {
        for (int i = 0; i < heroesCombatantHUD.Length; i++)
        {
            heroesCombatantHUD[i].CheckIfAffected(target, selectedEquip, user);
        }
        for (int i = 0; i < enemiesCombatantHUD.Length; i++)
        {
            enemiesCombatantHUD[i].CheckIfAffected(target, selectedEquip, user);
        }
    }

    public void OnTargetBtnHoverExit(TargetBtn targetBtn)
    {
        FindObjectOfType<PlayerInputManager>().HoverTargetBtnExit(targetBtn);
    }

    public void startTurnOf(Character character)
    {
        foreach (TileUI combatantHUD in heroesCombatantHUD)
        {
            if (combatantHUD.getCharacter() == character)
            {
                combatantHUD.showTargetBtnWithColor(Color.grey);
            }
            else
            {
                combatantHUD.HideTargetBtn();
            }
        }
        foreach (TileUI combatantHUD in enemiesCombatantHUD)
        {
            if (combatantHUD.getCharacter() == character)
            {
                combatantHUD.showTargetBtnWithColor(Color.grey);
            }
            else
            {
                combatantHUD.HideTargetBtn();
            }
        }
    }

    public void endTurnOf(Character character)
    {
        character.getHUD().getAnimator().SetBool("Destaque", false);
    }

    public static CombHUDManager getInstance() { return instance; }


    public TileUI[] getHeroesCombatantHUD() { return heroesCombatantHUD; }
    public TileUI[] getEnemiesCombatantHUD() { return enemiesCombatantHUD; }
}