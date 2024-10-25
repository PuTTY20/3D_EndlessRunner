using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    Transform canvasRank;
    [SerializeField] Text score_txt;
    Text speedUp_txt;
    Button retryBtn;
    Button exitBtn;
    Text rankTxt;

    readonly string speedUp = "SPEED UP!";
    int lastScore = -1;

    void Start()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        score_txt = canvas.GetChild(0).GetChild(0).GetComponent<Text>();
        speedUp_txt = canvas.GetChild(1).GetComponent<Text>();
        exitBtn = canvas.GetChild(2).GetComponent<Button>();
        retryBtn = canvas.GetChild(3).GetChild(0).GetComponent<Button>();

        canvasRank = GameObject.Find("Canvas_Rank").transform;
        rankTxt = canvasRank.GetChild(0).GetChild(0).GetComponent<Text>();

        retryBtn.onClick.AddListener(() => RetryGame());
        exitBtn.onClick.AddListener(() => ExitGame());

        speedUp_txt.gameObject.SetActive(false);

        canvas.gameObject.SetActive(true);
        canvasRank.gameObject.SetActive(false);
    }

    void Update()
    {
        score_txt.text = $"{GameManager.Score.score}M";
        CheckScore();
    }

    private void CheckScore()
    {
        if ((GameManager.Score.score == 100 || GameManager.Score.score == 200) && GameManager.Score.score != lastScore)
        {
            lastScore = GameManager.Score.score; //프레임동안 값이 같아져 1번만 함
            speedUp_txt.gameObject.SetActive(true);
            StartCoroutine(ShowTextEffect(speedUp));
        }
    }

    IEnumerator ShowTextEffect(string text)
    {
        speedUp_txt.text = string.Empty;

        foreach (char c in text)
        {
            speedUp_txt.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.8f);
        speedUp_txt.gameObject.SetActive(false);
    }

    public void ShowRanking()
    {
        canvasRank.gameObject.SetActive(true);
        rankTxt.text = GameManager.Score.score.ToString();
    }

    void RetryGame() => GameManager.instance.Retry();

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}