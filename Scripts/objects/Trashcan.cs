using UnityEngine;
using UnityEngine.SceneManagement;

public class Trashcan : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null && inventory.heldGarbage != null)
        {
            inventory.DropGarbageBag();
            Destroy(inventory.heldGarbage?.gameObject);
            inventory.heldGarbage = null;
            SceneManager.LoadScene("Hangman");
        }
    }

    public void OnHoverEnter() { }
    public void OnHoverExit() { }
}
