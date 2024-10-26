using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    RemyCtrl _remy;

    public int score = 0;
    float deciamlScore = 0f;
    float addScore = 0f;
    float initAddScore = 1f;
    float midAddScore = 1.5f;
    float maxAddScore = 2.25f;

    void Start()
    {
        addScore = initAddScore;

        _remy = FindObjectOfType<RemyCtrl>();
    }

    void Update()
    {
        if (_remy.isPlatform && !GameManager.instance.isDie && !GameManager.instance.isReset)
            AddToScore();
    }

    void AddToScore()
    {
        if (score >= 200)
            addScore = maxAddScore;
        else if (score >= 100)
            addScore = midAddScore;

        deciamlScore += Time.deltaTime * addScore; // 초당 1씩 증가

        if (deciamlScore >= 1f)
        {
            score += (int)deciamlScore; // 정수 부분만큼 점수(score)에 더함
            deciamlScore -= Mathf.Floor(deciamlScore); // 정수로 더해진 값 만큼 deciamlScore에서 뺌
        }
    }

    public void ResetScore()
    {
        score = 0;
        deciamlScore = 0f;
        addScore = initAddScore;
    }
}