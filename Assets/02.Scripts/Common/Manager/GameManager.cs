using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public static UIManager UI;
    public static ScoreManager Score;
    public static ObjectManager Object;
    public static ObjectPooling Pooling;

    RemyCtrl _remy;
    MoveStart _startplatform;
    RemyDamage _remyDamage;

    public bool isDie = false;
    public bool isReset = false;
    public bool isRankReset = false;
    public bool isInvincible = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        UI = gameObject.AddComponent<UIManager>();
        Score = gameObject.AddComponent<ScoreManager>();
        Object = gameObject.AddComponent<ObjectManager>();
        Pooling = gameObject.AddComponent<ObjectPooling>();
    }

    void Start()
    {
        _remy = FindObjectOfType<RemyCtrl>();
        _startplatform = FindObjectOfType<MoveStart>();
        _remyDamage = _remy.GetComponent<RemyDamage>();
    }

    void Update()
    {
        if (isDie)
        {
            UI.gaugeImg.fillAmount = 0;
            Score.SaveScore();
            UI.SetRank();
            UI.OnOffRank(true);
            Object.OffAllObject();
        }

        if (isRankReset)
            Score.ResetRank();
    }

    public void Reset()
    {
        isReset = true;
        isDie = false;

        Object.OffAllObject();

        StartCoroutine(Object.ActivatePlatforms());

        UI.gaugeImg.fillAmount = 0;
        UI.OnOffRank(false);
        Score.ResetScore();
        _remy.ResetRemy();
        _startplatform.ResetStartPlatform();
        _remyDamage.ResetHP();

        isReset = false;
    }
}