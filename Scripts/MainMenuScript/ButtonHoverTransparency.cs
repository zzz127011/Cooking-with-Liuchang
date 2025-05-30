using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverTransparency : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverAlpha = 0.5f;
    public float normalAlpha = 1f;
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            var color = buttonImage.color;
            color.a = hoverAlpha;
            buttonImage.color = color;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            var color = buttonImage.color;
            color.a = normalAlpha;
            buttonImage.color = color;
        }
    }
}