using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour/*, IPlayAnimationByString*/
{

    //TODO Delete Animator from Prefab
    [SerializeField] private Character combatant;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Text hpNumbers;
    [SerializeField] private TargetBtn targetButton;
    [SerializeField] private Text buffText;
    [SerializeField] private DamageFB damageFbPrefab;
    [SerializeField] private Text initiative;
    [SerializeField] private Text name;
    private RectTransform rectTransform;
    [SerializeField] RectTransform portraitHandler;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(Tile tile)
    {
        if (tile.isFromHero())
        {
            hpNumbers.gameObject.SetActive(true);
        }
        else
        {
            portraitHandler.localScale = new Vector3(-1, 1, 1);
            hpNumbers.gameObject.SetActive(false);
        }
        if (tile.getOccupant() != null)
        {
            combatant = tile.getOccupant();
            combatant.transform.SetParent(portraitHandler);
            combatant.transform.localPosition = new Vector3(0, -50);
            hpBar.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
            name.text = combatant.getName();
            combatant.OnHUDValuesChange += Refresh;
            combatant.OnHPValuesChange += HPFeedback;
            Refresh();
        }
        else
        {
            name.gameObject.SetActive(false);
            this.combatant = null;
            hpBar.gameObject.SetActive(false);
            hpNumbers.gameObject.SetActive(false);
            if (targetButton != null)
                targetButton.Disappear();
        }
        targetButton.setTile(tile);
    }

    public void Deinitialize()
    {
        if (combatant != null)
        {
            foreach (Transform transform in portraitHandler.transform)
            {
                transform.SetParent(null);
            }
            combatant.OnHUDValuesChange -= Refresh;
            combatant.OnHPValuesChange -= HPFeedback;
            combatant = null;
        }
    }

    public void HPFeedback(int pastHp, int amountChanged, bool wasCritic)
    {
        DamageFB damageFb = Instantiate(damageFbPrefab);
        damageFb.transform.SetParent(transform.parent, false);
        damageFb.getRectTransform().localPosition = rectTransform.localPosition + Vector3.right * 50;
        damageFb.Initialize(amountChanged, wasCritic);
    }

    public void setHpBar(float v)
    {
        if (v >= 0 && v <= 1)
            hpBar.value = v;
    }

    public void setHpNumbers(float hp, float maxHp)
    {
        hpNumbers.text = hp + "/" + maxHp;
    }

    public void Refresh()
    {
        setHpBar(combatant.getHp() / combatant.getMaxHp());
        setHpNumbers(combatant.getHp(), combatant.getMaxHp());
    }

    public void ShowTargetBtn(Character user, Equip skill)
    {
        if (enabled)
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

    public void ShowPossibleInitiative(Character activeCharacter, Equip skill)
    {
        DungeonManager dungeonManager = DungeonManager.getInstance();

    }

    public void showTargetBtnWithColor(Color color)
    {
        targetButton.Show(color);
    }

    public void CheckIfAffected(Tile target, Equip choosedSkill, Character user)
    {
        if (enabled)
            targetButton.CheckIfAffected(target, choosedSkill, user);
    }

    void OnDisable()
    {
        if (combatant != null)
        {
            combatant.OnHUDValuesChange -= Refresh;
        }
    }

    public Character getCharacter() { return combatant; }
}
