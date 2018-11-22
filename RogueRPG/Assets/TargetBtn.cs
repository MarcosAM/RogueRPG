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

    public void Appear(Character user, Skill skill)
    {
        if (tile.IsAvailable())
        {
            HideHitPreview();

            if (skill.IsTargetable(user, tile))
            {
                image.gameObject.SetActive(true);
                image.color = Color.gray;
                button.interactable = true;
            }
            else
            {
                image.gameObject.SetActive(false);
                button.interactable = false;
            }
        }
    }

    public void CheckIfAffected(Tile target, Skill skill, Character user)
    {
        if (tile.IsAvailable())
        {
            if (skill.UniqueEffectWillAffect(user, target, tile))
            {
                image.gameObject.SetActive(true);
                button.interactable = true;
                image.color = Color.black;
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
