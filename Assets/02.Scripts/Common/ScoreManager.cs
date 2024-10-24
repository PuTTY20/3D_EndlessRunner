using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    RemyCtrl _remy;

    public int score = 0;
    float deciamlScore = 0f;
    float addScore = 0f;
    float initAddScore = 1f;
    float midAddScore = 1.5f;
    float maxAddScore = 2.25f;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        addScore = initAddScore;

        _remy = GameObject.Find("Remy").GetComponent<RemyCtrl>();
    }

    void Update()
    {
        if (!_remy.isDie)
            AddToScore();
    }

    void AddToScore()
    {
        if (score >= 200)
        {
            addScore = maxAddScore;
            Debug.Log(addScore);
        }
        else if (score >= 100)
        {
            addScore = midAddScore;
            Debug.Log(addScore);
        }

        deciamlScore += Time.deltaTime * addScore; // 초당 1씩 증가

        if (deciamlScore >= 1f)
        {
            score += (int)deciamlScore; // 정수 부분만큼 점수(score)에 더함
            deciamlScore -= Mathf.Floor(deciamlScore); // 정수로 더해진 값 만큼 deciamlScore에서 뺌
        }
    }

    public void ResetGame()
    {
        score = 0;
        deciamlScore = 0f;
        addScore = initAddScore;
    }
}