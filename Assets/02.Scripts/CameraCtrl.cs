using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    RemyCtrl _remy;
    Transform Camtr;
    CamShake camShake; // CamShake 스크립트 참조

    Vector3 initCamPos;
    float damping = 5f;

    void Start()
    {
        Camtr = Camera.main.transform;
        _remy = GameObject.Find("Remy").GetComponent<RemyCtrl>();
        camShake = _remy.GetComponent<CamShake>(); // Remy 오브젝트에서 CamShake 스크립트 가져오기
        initCamPos = Camtr.position;
    }

    void Update()
    {
        if (!_remy.isGround)
        {
            // up y pos of camera to 1
            Camtr.position = Vector3.Lerp(Camtr.position, new Vector3(Camtr.position.x, initCamPos.y + 1f, Camtr.position.z), Time.deltaTime * damping);
        }
        else if (_remy.isSlide)
        {
            // down y pos of camera to 0.5
            Camtr.position = Vector3.Lerp(Camtr.position, new Vector3(Camtr.position.x, initCamPos.y - 1.5f, Camtr.position.z), Time.deltaTime * damping);
        }
        else
            Camtr.position = Vector3.Lerp(Camtr.position, initCamPos, Time.deltaTime * 10f);
    }
}