using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    RectTransform backgroundRect;
    Text text;

    static Tooltip instance;

    void Awake()
    {
        instance = this;

        text = GetComponentInChildren<Text>();
        HideSelf();

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, Input.mousePosition, null, out localPoint);

        transform.localPosition = localPoint;
    }

    private void ShowSelf(string tip)
    {
        transform.SetParent(FindObjectOfType<Canvas>().transform);
        gameObject.SetActive(true);

        text.text = tip;
        Vector2 backgroundSize = new Vector2(text.preferredWidth + 8f, text.preferredHeight + 8f);
        backgroundRect.sizeDelta = backgroundSize;
    }

    private void HideSelf()
    {
        transform.SetParent(null);
        Debug.Log("Meu pai " + transform.parent);
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }


    public static void ShowTooltip(string tip)
    {
        instance.ShowSelf(tip);
    }

    public static void HideTooltip()
    {
        instance.HideSelf();
    }
}
