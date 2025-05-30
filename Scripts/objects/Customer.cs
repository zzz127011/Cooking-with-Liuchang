using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Customer : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public Transform targetPosition;
    public float moveSpeed = 2f;
    public Tray.TrayVariant wantedVariant;
    public TextMeshProUGUI diologText;
    public string customerText;
    private bool atCounter = false;
    private bool served = false;
    private float interactCooldown = 0f;
    public System.Action<int> OnCustomerDestroyed;
    public int spawnIndex;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
            rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
        wantedVariant = (Tray.TrayVariant)Random.Range(1, System.Enum.GetValues(typeof(Tray.TrayVariant)).Length);

        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();

        if (targetPosition == null)
            targetPosition = GameObject.Find("CustomerPlacement")?.transform;
        if (diologText == null)
            diologText = GameObject.Find("RealDiolog")?.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Fix: Reduce cooldown timer
        if (interactCooldown > 0f)
            interactCooldown -= Time.deltaTime;

        if (!atCounter)
        {
            MoveToCounter();
        }
    }

    void MoveToCounter()
    {
        if (targetPosition == null) return;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
        {
            atCounter = true;
        }
    }

    public void Interact()
    {
        if (interactCooldown > 0f) return;
        interactCooldown = 0.5f;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            var inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                if (inventory.heldTray != null)
                {
                    Tray tray = inventory.heldTray.GetComponent<Tray>();
                    StartCoroutine(TryServeTray(tray));
                }
                else
                {
                    if (diologText != null)
                        diologText.text = "You need to hold a tray to serve";
                }
            }
        }
    }

    public void OnHoverEnter()
    {
        customerText = $"I want {wantedVariant}";
        if (rend != null)
            rend.material.color = highlightColor;
        if (diologText != null)
            diologText.text = customerText;
    }

    public void OnHoverExit()
    {
        if (rend != null)
            rend.material.color = originalColor;
        if (diologText != null)
            diologText.text = "";
    }

    public IEnumerator TryServeTray(Tray tray)
    {
        if (served) yield break;

        if (tray != null && tray.isCooked && tray.variant == wantedVariant)
        {
            served = true;
            if (diologText != null)
                diologText.text = "Thank you";

            GameObject invisTrayObj = GameObject.FindObjectOfType<InvisTray>()?.gameObject;
            if (invisTrayObj != null)
            {
                var player = GameObject.FindWithTag("Player");
                var inventory = player?.GetComponent<PlayerInventory>();
                if (inventory != null && inventory.heldTray != null)
                {
                    GameObject droppedTray = inventory.DropTray();
                    if (droppedTray != null)
                    {
                        Tray droppedTrayScript = droppedTray.GetComponent<Tray>();
                        if (droppedTrayScript != null)
                        {
                            droppedTrayScript.variant = Tray.TrayVariant.None;
                            droppedTrayScript.isCooked = false;
                        }
                        droppedTray.transform.SetParent(null);
                        droppedTray.transform.position = invisTrayObj.transform.position;
                        droppedTray.transform.rotation = invisTrayObj.transform.rotation;
                        droppedTray.SetActive(true);
                    }
                }
            }

            StartCoroutine(LeaveAfterDelay());
        }
        else
        {
            if (diologText != null)
                diologText.text = "That's not what I ordered!";
            yield return new WaitForSeconds(2f);
            if (diologText != null)
            {
                customerText = $"I want {wantedVariant}";
                diologText.text = customerText;
            }
        }
    }

    IEnumerator LeaveAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        var spawner = FindAnyObjectByType<CustomerSpawner>();
        var player = FindAnyObjectByType<Player>();
        if (spawner != null && player != null)
        {
            if (player.currentCustomerSpawnIndex < 3)
            {
                player.currentCustomerSpawnIndex++;
                spawner.SpawnCustomerAtIndex(player.currentCustomerSpawnIndex);
            }
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (OnCustomerDestroyed != null)
            OnCustomerDestroyed.Invoke(spawnIndex);
    }
}