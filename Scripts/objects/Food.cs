using UnityEngine;
using TMPro;

public class Food : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public Tray.TrayVariant variantToAdd;
    public TextMeshProUGUI diologText;
    public GameObject player;


    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void OnHoverEnter()
    {
        rend.material.color = highlightColor;
        diologText.text = $"This is {variantToAdd} | Click to add it to the tray";
    }

    public void OnHoverExit()
    {
        rend.material.color = originalColor;
        diologText.text = "";

    }

    public System.Collections.IEnumerator AddToTray(Tray tray)
    {
        if (tray != null)
        {
            if (variantToAdd == Tray.TrayVariant.NormalWithEggs)
            {
                if (tray.variant == Tray.TrayVariant.Normal)
                {
                    tray.variant = Tray.TrayVariant.NormalWithEggs;
                }
                else
                {
                    diologText.text = "Tray already has eggs or isn't a normal tray";
                    yield return new WaitForSeconds(2f);
                    diologText.text = "";
                }
                
            }
            else
            {
                tray.variant = variantToAdd;
            }
            
        }
    }

    public void Interact()
    {
        if (player != null)
        {
            var inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.heldTray != null)
            {
                Debug.Log("Player is holding a tray!");
                Tray tray = inventory.heldTray.GetComponent<Tray>();
                if (tray != null)
                {
                    tray.variant = variantToAdd;
                    Debug.Log("Tray variant changed to: " + tray.variant);
                }
            }
            else
            {
                Debug.LogWarning("Player is NOT holding a tray!");
            }
        }
    }
}

