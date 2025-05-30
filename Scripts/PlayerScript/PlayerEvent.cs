using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int currentCustomerSpawnIndex = 0;
    public void SpawnCustomer()
    {
        var spawner = FindAnyObjectByType<CustomerSpawner>();
        if (spawner != null)
        {
            spawner.SpawnCustomerAtIndex(currentCustomerSpawnIndex);
        }
    }
}
