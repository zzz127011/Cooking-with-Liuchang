using UnityEngine;

public class AttachObject : MonoBehaviour
{
    public void AttachToPlayer(GameObject objectToAttach)
    {
        var inventory = GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.PickUpTray(objectToAttach);
        }
    }

    public void DetachFromPlayer(GameObject objectToDetach)
    {
        var inventory = GetComponent<PlayerInventory>();
        if (inventory != null && inventory.heldTray == objectToDetach)
        {
            inventory.DropTray();
        }
    }
}
