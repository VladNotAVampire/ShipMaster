using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CartCameraController : ICameraController
{
    public CartCameraController(PlayerCameraScript player)
    {
        camX = player.cam.parent;
        camY = camX.parent;
        y = player.transform.rotation.y;
        x = 0;
        mouseSensivity = player.mouseSensivity;
    }

    public float x { get; set; }
    public float y { get; set; }
    public float mouseSensivity;

    private Transform camX;
    private Transform camY;

    public void SetCameraRotation()
    {
        y += Input.GetAxis("Mouse X") * mouseSensivity * Time.fixedDeltaTime;
        x -= Input.GetAxis("Mouse Y") * mouseSensivity * Time.fixedDeltaTime;
        x = Mathf.Clamp(x, -90, 90);

        camY.localRotation = Quaternion.Euler(
             camY.rotation.x,
             y,
             camY.rotation.z);

        camX.localRotation = Quaternion.Euler(
             x,
             camX.localRotation.y,
             camX.localRotation.z);
    }
}
