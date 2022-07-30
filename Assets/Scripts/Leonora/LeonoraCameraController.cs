using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LeonoraCameraController : CameraController
{
    [SerializeField] private CinemachineFreeLook _camera;


    public override void UpdateCamera(float x, float y)
    {
        _camera.m_XAxis.Value += x;
        _camera.m_YAxis.Value += y;
    }
}
