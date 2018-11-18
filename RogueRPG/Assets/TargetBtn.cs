using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TargetBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Tile tile;
    public Image image;
    Button button;
    [SerializeField] RectTransform hitPreview;

    void Awake()
    {
        tile = GetComponentInParent<Tile>();
        button = GetComponent<Button>();
        Disappear();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        TargetBtnsManager.GetInstance().OnTargetBtnPressed(tile);
    }

    public void Appear(Character user, Equip skill)
    {
        if (tile.IsAvailable())
        {
            HideHitPreview();
            Skill skillEffect;
            if (tile.GetSide() == user.IsPlayable())
            {
                if (tile.GetRow() == user.GetPosition())
                {
                    skillEffect = skill.GetSelfEffect();
                    image.color = new Color(0.309f, 0.380f, 0.674f, 1);
                }
                else
                {
                    skillEffect = skill.GetAlliesEffect();
                    image.color = new Color(0.952f, 0.921f, 0.235f, 1);
                }
            }
            else
            {
                if ((Mathf.Abs(tile.GetRow() - user.GetPosition()) <= skill.GetMeleeEffect().GetRange()) && tile.GetCharacter() != null)
                {
                    skillEffect = skill.GetMeleeEffect();
                    image.color = new Color(0.925f, 0.258f, 0.258f, 1);
                }
                else
                {
                    skillEffect = skill.GetRangedEffect();
                    image.color = new Color(0.427f, 0.745f, 0.266f, 1);
                }
            }

            if (tile.GetCharacter() != null)
            {
                if (tile.GetCharacter().IsAlive())
                {
                    image.gameObject.SetActive(true);
                    button.interactable = true;
                }
                else
                {
                    image.gameObject.SetActive(false);
                    button.interactable = false;
                }
            }
            else
            {
                if (skillEffect is SAtkRA)
                {
                    image.gameObject.SetActive(true);
                    button.interactable = true;
                }
                else
                {
                    image.gameObject.SetActive(false);
                    button.interactable = false;
                }
                if (skillEffect is SMove)
                {
                    if (Mathf.Abs(tile.GetRow() - user.GetPosition()) <= skillEffect.GetRange())
                    {
                        image.gameObject.SetActive(true);
                        button.interactable = true;
                    }
                }
            }
        }

    }

    public void CheckIfAffected(Tile target, Equip equip, Character user)
    {
        if (tile.IsAvailable())
        {
            //Skill skill;
            //if (target.GetSide() == user.IsPlayable())
            //{
            //    if (target.GetRow() == user.getPosition())
            //    {
            //        skill = choosedSkill.GetSelfEffect();
            //        image.color = new Color(0.309f, 0.380f, 0.674f, 1);
            //    }
            //    else
            //    {
            //        skill = choosedSkill.GetAlliesEffect();
            //        image.color = new Color(0.952f, 0.921f, 0.235f, 1);
            //    }
            //}
            //else
            //{
            //    if ((Mathf.Abs(target.GetRow() - user.getPosition()) <= choosedSkill.GetMeleeEffect().GetRange()) && target.GetCharacter() != null)
            //    {
            //        skill = choosedSkill.GetMeleeEffect();
            //        image.color = new Color(0.925f, 0.258f, 0.258f, 1);
            //    }
            //    else
            //    {
            //        skill = choosedSkill.GetRangedEffect();
            //        image.color = new Color(0.427f, 0.745f, 0.266f, 1);
            //    }
            //}

            //if (skill.WillBeAffected(user, target, tile))
            //{
            //    image.gameObject.SetActive(true);
            //    button.interactable = true;
            //    if (skill is SAtk && tile.CharacterIsAlive())
            //        ShowHitPreview(((SAtk)skill).ProbabilityToHit(user, target, tile));
            //}
            //else
            //{
            //    image.gameObject.SetActive(false);
            //    button.interactable = false;
            //}
            TargetBtnStatus status = equip.AppropriateSkill(user, target).GetTargetBtnStatus(user, target, this.tile, equip);
            if (status.color != null)
            {
                image.color = (Color)status.color;
                if (status.probability != null)
                    ShowHitPreview((float)status.probability);
            }
            else
            {
                image.gameObject.SetActive(false);
                button.interactable = false;
            }
        }
    }

    public void Show(Color color)
    {
        if (tile.IsAvailable())
        {
            image.gameObject.SetActive(true);
            button.interactable = false;
            image.color = color;
        }
    }

    public void Disappear()
    {
        button.interactable = false;
        image.gameObject.SetActive(false);
        HideHitPreview();
    }

    void ShowHitPreview(float hit)
    {
        if (tile.IsAvailable())
        {
            hitPreview.gameObject.SetActive(true);
            hitPreview.GetComponentInChildren<Text>().text = (hit * 100f).ToString() + "%";
        }
    }

    void HideHitPreview()
    {
        hitPreview.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (tile.IsAvailable())
            TargetBtnsManager.GetInstance().OnTargetBtnHoverEnter(this);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (tile.IsAvailable())
            TargetBtnsManager.GetInstance().OnTargetBtnHoverExit(this);
    }

    public Tile getTile() { return tile; }

    public struct TargetBtnStatus
    {
        public Color? color;
        public float? probability;

        public TargetBtnStatus(Color? color = null, float? probability = null)
        {
            this.color = color;
            this.probability = probability;
        }
    }
}
