using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static UIManager UI;
    public static ScoreManager Score;
    public static PlatformManager Platform;
    public static ObjectPooling Pooling;

    CameraCtrl _cam;
    RemyCtrl _remy;
    MoveObject _moveObject;

    readonly string remy = "Remy";
    public readonly string startPlatform = "Start";

    void Awake()
    {
        UI = gameObject.AddComponent<UIManager>();
        Score = gameObject.AddComponent<ScoreManager>();
        Platform = gameObject.AddComponent<PlatformManager>();
        Pooling = gameObject.AddComponent<ObjectPooling>();
    }

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
            Score.SaveScore();
            UI.ShowRanking();
        }
    }

    public void Retry()
    {
        _cam.ResetCamera();
        _remy.ResetRemy();
        _moveObject.ResetPlatform();
        Score.ResetGame();
        ObjectPooling.objpooling.OffAllPlatform();
    }
}
