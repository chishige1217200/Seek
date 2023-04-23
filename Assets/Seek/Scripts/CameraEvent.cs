using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour
{
    CameraController cameraController;
    [SerializeField] Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
            if (cameraController != null)
            {
                if (cameraController.cameraAuto) cameraController.cameraAuto = false;
                cameraController.ManualView(pos);
            }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
            if (cameraController != null)
                if (!cameraController.cameraAuto) cameraController.cameraAuto = true;
    }
}
