using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Phone : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public TextMeshProUGUI fakeDiologText;
    public TextMeshProUGUI diologText;
    public CustomerSpawner customerSpawner; 
    private bool hasCalledManager = false;
    private bool waitingForLastCustomer = false;
    private int spawnedCount = 0;
    private const int maxCustomers = 5;
    public GameObject garbageBagPrefab;
    public Transform garbageBagSpawnPoint;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        if (customerSpawner == null)
        {
            customerSpawner = FindObjectOfType<CustomerSpawner>();
        }
    }

    public void Interact()
    {
        if (!hasCalledManager)
        {
            StartCoroutine(ShowPhoneMessage());
        }
        else
        {
            diologText.text = "The manager is unavailable right now.";
        }
    }

    private System.Collections.IEnumerator ShowPhoneMessage()
    {
        hasCalledManager = true;
        diologText.text = "Calling manager...";
        yield return new WaitForSeconds(1.5f);
        diologText.text = "Manager: Hello, this is your manager";
        yield return new WaitForSeconds(3f);
        diologText.text = "Manager: Get ready for the next customer";
        yield return new WaitForSeconds(3f);
        diologText.text = "Manager: Go to the tray and pick up the food";
        yield return new WaitForSeconds(3f);
        diologText.text = "Manager: Go to the tray and pick up the food";
        yield return new WaitForSeconds(3f);
        diologText.text = "Manager: Put it in the oven and serve it to the customer";
        yield return new WaitForSeconds(3f);
        diologText.text = "Manager: That's all for now, have fun kid";
        yield return new WaitForSeconds(3f);
        diologText.text = ".....";
        yield return new WaitForSeconds(2f);
        diologText.text = "";
        if (customerSpawner != null && spawnedCount < maxCustomers)
        {
            customerSpawner.SpawnCustomerAtIndex(0);
            spawnedCount++;
            waitingForLastCustomer = true;
            while (GameObject.FindObjectsOfType<Customer>().Length > 0)
            {
                yield return null;
            }
            waitingForLastCustomer = false;
            while (spawnedCount < maxCustomers)
            {
                customerSpawner.SpawnCustomerAtIndex(0);
                spawnedCount++;
                waitingForLastCustomer = true;
                while (GameObject.FindObjectsOfType<Customer>().Length > 0)
                {
                    yield return null;
                }
                waitingForLastCustomer = false;
            }
            SceneManager.LoadScene("Hangman");
        }
    }

    public void OnHoverEnter()
    {
        rend.material.color = highlightColor;
        fakeDiologText.text = "Phone";
    }

    public void OnHoverExit()
    {
        rend.material.color = originalColor;
        fakeDiologText.text = "";
    }
}

