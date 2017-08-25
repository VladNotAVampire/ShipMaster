using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StandartCameraController : ICameraController
{
    public StandartCameraController(PlayerCameraScript player, float x, float y)
    {
        this.player = player;
        cam = player.cam.parent;
        mouseSensivity = player.mouseSensivity;
        this.y = y;
        this.x = x;

        cam.parent.rotation = player.transform.rotation;
    }

    public StandartCameraController(PlayerCameraScript player)
        : this(player, 0, player.transform.eulerAngles.y)
    {
    }

    private PlayerCameraScript player;
    private Transform cam;
    public float mouseSensivity;
    public float y { get; set; }
    public float x { get; set; }

    public void SetCameraRotation()
    {
        y += Input.GetAxis("Mouse X") * mouseSensivity * Time.fixedDeltaTime;
        x -= Input.GetAxis("Mouse Y") * mouseSensivity * Time.fixedDeltaTime;
        x = Mathf.Clamp(x, -90, 90);

        player.transform.localRotation = Quaternion.Euler(
             player.transform.rotation.x,
             y,
             player.transform.rotation.z);

        cam.localRotation = Quaternion.Euler(
             x,
             cam.localRotation.y,
             cam.localRotation.z);
    }
}
