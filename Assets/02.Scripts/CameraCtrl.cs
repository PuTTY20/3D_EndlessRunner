using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform Target;
    public float Height = 2.5f;
    public float Distance = 6f;
    float Damping = 5f;
    Transform Camtr;
    Quaternion rot = Quaternion.identity;

    void Start()
    {
        Camtr = Camera.main.transform;
    }

    void LateUpdate()
    {
        float angle = Mathf.LerpAngle(Target.eulerAngles.y, Camtr.eulerAngles.y, Time.deltaTime * Damping);
        rot = Quaternion.Euler(0f, angle, 0f);
        Camtr.position = Target.position - (Vector3.forward * Distance) + (rot * Vector3.up * Height);
        Camtr.LookAt(Target.transform);
    }
}