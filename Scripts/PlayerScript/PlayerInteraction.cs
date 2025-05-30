using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float rayDistance = 5f;

    private IInteractable currentInteractable;

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    currentInteractable?.OnHoverExit();
                    currentInteractable = interactable;
                    currentInteractable.OnHoverEnter();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Interacting with: " + interactable);
                    interactable.Interact();
                }

                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable.OnHoverExit();
            currentInteractable = null;
        }
    }
}
