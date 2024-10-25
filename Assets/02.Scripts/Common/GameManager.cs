using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    CameraCtrl _cam;
    RemyCtrl _remy;
    MoveObject _moveObject;

    readonly string remy = "Remy";
    public readonly string startPlatform = "Start";

    void Start()
    {
        _cam = Camera.main.GetComponent<CameraCtrl>();
        _remy = GameObject.Find(remy).GetComponent<RemyCtrl>();
        _moveObject = GameObject.Find(startPlatform).GetComponent<MoveObject>();
    }

    void Update()
    {
        if(_remy.isDie)
        {
            // 게임오버 처리(score 저장, name 띄움, 랭킹 등)
        }
    }

    public void Retry()
    {
        _cam.ResetCamera();
        _remy.ResetRemy();
        _moveObject.ResetPlatform();
        ScoreManager.instance.ResetGame();
        ObjectPooling.poolingManager.OffAllPlatform();
    }
}
