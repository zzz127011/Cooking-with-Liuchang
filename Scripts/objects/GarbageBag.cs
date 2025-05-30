using UnityEngine;

public class GarbageBag : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null)
        {
            if (inventory.PickUpGarbageBag(this))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnHoverEnter() { }
    public void OnHoverExit() { }
}
