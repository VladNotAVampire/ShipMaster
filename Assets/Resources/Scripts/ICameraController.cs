using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ICameraController
{
    void SetCameraRotation();
    float x { get; set; }
    float y { get; set; }
}
