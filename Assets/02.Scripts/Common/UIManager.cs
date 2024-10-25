using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    Transform canvasRank;
    GameObject rankPanel;
    Button replay;
    Button exitBtn;
    Text score_txt;
    Text speedUp_txt;
    Text rankTxt;

    readonly string speedUp = "SPEED UP!";
    int lastScore = -1;

    void Start()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        score_txt = canvas.GetChild(0).GetChild(0).GetComponent<Text>();
        speedUp_txt = canvas.GetChild(1).GetComponent<Text>();
        exitBtn = canvas.GetChild(2).GetComponent<Button>();
        replay = canvas.GetChild(3).GetChild(0).GetComponent<Button>();

        canvasRank = GameObject.Find("Canvas_Rank").transform;
        rankTxt = canvasRank.GetChild(0).GetChild(0).GetComponent<Text>();

        replay.onClick.AddListener(() => ReplayGame());
        exitBtn.onClick.AddListener(() => ExitGame());

        rankPanel = canvasRank.GetChild(0).gameObject;
        rankPanel.SetActive(false);
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
            speedUp_txt.enabled = true;
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
        speedUp_txt.enabled = false;
    }

    public void ShowRanking()
    {
        rankPanel.SetActive(true);
        rankTxt.text = GameManager.Score.score.ToString();
    }

    void ReplayGame() => GameManager.instance.Reset();

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}