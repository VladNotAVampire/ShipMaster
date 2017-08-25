using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{
    public Transform cam;
    public Transform camHolder;
    public float mouseSensivity;
    public ICameraController camController;
    public float camDistance;

    private void Start()
    {
        camController = new StandartCameraController(this);
        camDistance = - cam.localPosition.z;
        camHolder = cam.parent;
    }

    private void FixedUpdate()
    {
        camController.SetCameraRotation();

        camDistance -= Input.GetAxis("Mouse ScrollWheel");
        camDistance = Mathf.Clamp(camDistance, 3.5f, 8);

        RaycastHit hit;

        if (!Physics.Raycast(camHolder.position, cam.position - camHolder.position, out hit, camDistance)) //detecting if cam hits objects
            cam.localPosition = new Vector3(0, camDistance * 0.1f, -camDistance);
        else
            cam.localPosition = new Vector3(0, (hit.distance - 0.5f) * 0.1f, -hit.distance + 0.5f);
    }
}
