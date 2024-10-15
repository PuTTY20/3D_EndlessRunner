using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    RemyCtrl _remy;
    public Transform Target;
    Transform Camtr;

    void Start()
    {
        Camtr = Camera.main.transform;
        Target = GameObject.Find("Remy").transform;
    }

    void Update()
    {
        if(!_remy.isGround)
        {

        }

        else if(_remy.isSlide)
        {
            //up y pos of camera to 1.5
        }
    }
}