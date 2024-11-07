using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    RemyCtrl _remy;

    List<int> scoreList;

    public int score = 0;
    float deciamlScore = 0f;
    float addScore = 0f;
    float initAddScore = 1f;
    float midAddScore = 1.5f;
    float maxAddScore = 2.5f;

    void Start()
    {
        addScore = initAddScore;

        _remy = FindObjectOfType<RemyCtrl>();
        scoreList = new List<int>();
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

    public void SaveScore()
    {
        List<int> scores = new List<int>();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey($"Score{i}"))
                scores.Add(PlayerPrefs.GetInt($"Score{i}"));
        }

        if (!scores.Contains(score))
            scores.Add(score);

        scores.Sort((a, b) => b.CompareTo(a)); // 내림차순 정렬

        for (int i = 0; i < 10; i++)
        {
            if (i < scores.Count)
                PlayerPrefs.SetInt($"Score{i}", scores[i]);

            else
                PlayerPrefs.DeleteKey($"Score{i}");
        }

        PlayerPrefs.Save();
    }

    public List<int> GetTopScores()
    {
        List<int> scores = new List<int>();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey($"Score{i}"))
                scores.Add(PlayerPrefs.GetInt($"Score{i}"));
        }

        return scores;
    }

    public void ResetScore()
    {
        score = 0;
        deciamlScore = 0f;
        addScore = initAddScore;
    }

    public void ResetRank()
    {
        for (int i = 0; i < 10; i++)
            PlayerPrefs.DeleteKey($"Score{i}");
            
        PlayerPrefs.Save();
    }
}