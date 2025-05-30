using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Oven : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public TextMeshProUGUI diologText;
    public float cookTime = 5f;

    private GameObject trayInOven = null;
    private bool isCooking = false;
    private bool trayReady = false;

    private bool interactionCooldown = false;
    private float cooldownTime = 0.2f; // 200 ms

    public GameObject player;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        rend.material.color = Color.green; 
    }

    public void Interact()
    {
        if (interactionCooldown) return;

        StartCoroutine(InteractionCooldown());

        var playerInventory = player?.GetComponent<PlayerInventory>();
        if (!isCooking && !trayReady)
        {
            if (playerInventory != null && playerInventory.heldTray != null)
            {
                Tray trayScript = playerInventory.heldTray.GetComponent<Tray>();
                if (trayScript != null && trayScript.isCooked)
                {
                    if (diologText != null)
                        diologText.text = "This tray is already cooked!";
                    return;
                }

                if (trayScript != null && trayScript.variant == Tray.TrayVariant.None)
                {
                    if (diologText != null)
                        diologText.text = "You need to add food to the tray before cooking";
                    return;
                }

                trayInOven = playerInventory.heldTray;
                trayInOven.transform.SetParent(null);
                trayInOven.SetActive(false);
                playerInventory.heldTray = null;
                StartCoroutine(CookTray());
            }
            else
            {
                if (diologText != null)
                    diologText.text = "You need a tray to cook";
            }
        }
        else if (trayReady)
        {
            if (playerInventory != null && trayInOven != null)
            {
                trayInOven.SetActive(true);
                Tray trayScript = trayInOven.GetComponent<Tray>();
                if (trayScript != null)
                    trayScript.isCooked = true;

                playerInventory.PickUpTray(trayInOven);
                trayInOven = null;
                trayReady = false;
                rend.material.color = Color.green;
                if (diologText != null)
                    diologText.text = "Tray is done";
            }
        }
        else
        {
            if (diologText != null)
                diologText.text = "Tray is still cooking...";
        }
    }

    private IEnumerator CookTray()
    {
        isCooking = true;
        trayReady = false;

        rend.material.color = Color.yellow;

        float elapsed = 0f;
        while (elapsed < cookTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        isCooking = false;
        trayReady = true;

        rend.material.color = Color.red;

        if (diologText != null)
            diologText.text = "Tray is ready, click oven to take it out";
    }

    private IEnumerator InteractionCooldown()
    {
        interactionCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        interactionCooldown = false;
    }

    public void OnHoverEnter()
    {
        if (!isCooking && !trayReady)
            diologText.text = "This is an Oven | Click to cook a tray";
        else if (isCooking)
            diologText.text = "Tray is cooking...";
        else if (trayReady)
            diologText.text = "Tray is ready, click to take it out";
    }

    public void OnHoverExit()
    {
        diologText.text = "";
    }
}

