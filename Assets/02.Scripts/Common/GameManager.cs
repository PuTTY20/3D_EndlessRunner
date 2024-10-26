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

    RemyCtrl _remy;
    StartPlatform _startplatform;

    public bool isDie = false;
    public bool isReset = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        UI = gameObject.AddComponent<UIManager>();
        Score = gameObject.AddComponent<ScoreManager>();
        Platform = gameObject.AddComponent<PlatformManager>();
        Pooling = gameObject.AddComponent<ObjectPooling>();
    }

    void Start()
    {
        _remy = FindObjectOfType<RemyCtrl>();
        _startplatform = FindObjectOfType<StartPlatform>();
    }

    void Update()
    {
        if (isDie)
        {
            UI.ShowRanking(true);
        }
    }

    public void Reset()
    {
        isReset = true;

        isDie = false;
        UI.ShowRanking(false);
        Score.ResetScore();
        Platform.OffAllPlatform();
        _remy.ResetRemy();
        _startplatform.ResetStartPlatform();

        isReset = false;
    }
}