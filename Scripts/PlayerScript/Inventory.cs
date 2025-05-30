using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject heldTray;
    public Transform holdPoint;

    public GarbageBag heldGarbage;

    public bool PickUpTray(GameObject tray)
    {
        if (heldTray == null && tray != null)
        {
            heldTray = tray;
            tray.transform.SetParent(holdPoint);
            tray.transform.localPosition = new Vector3(0, 0, 0.5f);
            tray.transform.localRotation = Quaternion.identity;
            tray.SetActive(true);
            return true;
        }
        return false;
    }

    public GameObject DropTray()
    {
        if (heldTray != null)
        {
            GameObject tray = heldTray;
            heldTray = null;
            tray.transform.SetParent(null);
            tray.SetActive(true);
            return tray;
        }
        return null;
    }

    public bool PickUpGarbageBag(GarbageBag bag)
    {
        if (heldGarbage == null && bag != null)
        {
            heldGarbage = bag;
            bag.transform.SetParent(holdPoint);
            bag.transform.localPosition = new Vector3(0, 0, 0.5f);
            bag.transform.localRotation = Quaternion.identity;
            bag.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    public void DropGarbageBag()
    {
        if (heldGarbage != null)
        {
            heldGarbage.transform.SetParent(null);
            heldGarbage.gameObject.SetActive(true);
            heldGarbage = null;
        }
    }
}