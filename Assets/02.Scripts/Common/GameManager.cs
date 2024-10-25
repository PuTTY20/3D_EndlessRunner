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
    }

    void Update()
    {
        if (_remy.isDie)
        {
            Score.SaveScore();
            UI.ShowRanking();
        }
    }

    public void Reset()
    {
        _remy.ResetRemy();
        Score.ResetScore();
        Pooling.OffAllPlatform();
    }
}