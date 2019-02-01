using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFB : MonoBehaviour
{

    RectTransform rectTransform;
    Text text;
    Image image;
    [SerializeField] Sprite criticSprite;
    [SerializeField] Color damageColor;
    [SerializeField] Color healColor;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
    }

    public void Initialize(int value, bool wasCritic)
    {
        image.color = value > 0 ? healColor : damageColor;

        if (wasCritic)
        {
            image.sprite = criticSprite;
            image.rectTransform.sizeDelta = new Vector2(90, 90);
        }

        text.text = Mathf.Abs(value).ToString();
    }

    public void End()
    {
        Destroy(gameObject);
    }

    public RectTransform getRectTransform()
    {
        return rectTransform;
    }
}
