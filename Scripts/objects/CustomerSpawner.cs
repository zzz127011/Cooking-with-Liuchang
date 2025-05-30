using UnityEngine;
using System.Collections;
using TMPro;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform[] spawnPoints;
    public Transform counterPosition;
    public TextMeshProUGUI diologText;

    private int totalSpawned = 0;
    private const int maxCustomers = 5;

    private void Start()
    {
        if (counterPosition == null)
            counterPosition = GameObject.Find("CustomerPlacement")?.transform;

        if (diologText == null)
            diologText = GameObject.Find("RealDiolog")?.GetComponent<TextMeshProUGUI>();
    }

    public void SpawnCustomerAtIndex(int spawnIndex)
    {
        if (spawnPoints.Length == 0 || customerPrefab == null || counterPosition == null)
            return;

        if (spawnIndex < 0 || spawnIndex >= spawnPoints.Length)
            return;

        if (totalSpawned >= maxCustomers)
            return;

        GameObject customerObj = Instantiate(customerPrefab, spawnPoints[spawnIndex].position, Quaternion.Euler(0, 180, 0));
        Customer customerScript = customerObj.GetComponent<Customer>();
        if (customerScript == null)
            customerScript = customerObj.AddComponent<Customer>();
        customerScript.targetPosition = counterPosition;
        customerScript.moveSpeed = 2f;
        customerScript.diologText = diologText;
        customerScript.spawnIndex = spawnIndex;
        customerScript.OnCustomerDestroyed = (idx) => SpawnCustomerAtIndex(idx);
        totalSpawned++;
    }
}