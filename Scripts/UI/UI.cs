using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        if (cam == null) cam = Camera.main;
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
