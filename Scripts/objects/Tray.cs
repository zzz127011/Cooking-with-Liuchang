using UnityEngine;
using TMPro;

public class Tray : MonoBehaviour, IInteractable
{
    public enum TrayVariant
    {
        None,
        Normal,
        NormalWithEggs,
    }

    public TrayVariant variant = TrayVariant.None;

    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public GameObject player;
    public GameObject TheTray;
    public TextMeshProUGUI diologText;
    public bool isCooked = false;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        if (TheTray == null)
            TheTray = this.gameObject;
    }

    public void Interact()
    {
        AttachObject attachScript = player.GetComponent<AttachObject>();
        attachScript.AttachToPlayer(TheTray);
    }

    public void OnHoverEnter()
    {
        rend.material.color = highlightColor;
        diologText.text = $"This is a Tray ({variant}) | Click to pick it up";
    }

    public void OnHoverExit()
    {
        rend.material.color = originalColor;
        diologText.text = "";
    }
}

