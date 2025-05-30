using UnityEngine;
using TMPro;

public class InvisTray : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public TextMeshProUGUI diologText;
    public GameObject player;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Interact()
    {
        var inventory = player?.GetComponent<PlayerInventory>();
        if (inventory != null && inventory.heldTray != null)
        {
            GameObject droppedTray = inventory.DropTray();
            if (droppedTray != null)
            {
                Tray tray = droppedTray.GetComponent<Tray>();
                if (tray != null)
                {
                    tray.variant = Tray.TrayVariant.None;
                    tray.isCooked = false;
                }
                droppedTray.transform.SetParent(null);
                droppedTray.transform.position = this.transform.position;
                droppedTray.transform.rotation = this.transform.rotation;
                droppedTray.SetActive(true);
            }
        }
    }

    public void OnHoverEnter()
    {
        rend.material.color = highlightColor;
    }

    public void OnHoverExit()
    {
        rend.material.color = originalColor;
    }
}

