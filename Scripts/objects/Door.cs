using UnityEngine;
using TMPro;

public class HighlightOnHover : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    public TextMeshProUGUI diologText;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Interact()
    {
        
    }

    public void OnHoverEnter()
    {
        rend.material.color = highlightColor;
        diologText.text = "Door";
    }

    public void OnHoverExit()
    {
        rend.material.color = originalColor;
        diologText.text = "";
    }
}

