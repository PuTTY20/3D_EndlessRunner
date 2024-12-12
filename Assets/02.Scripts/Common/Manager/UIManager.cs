using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Transform canvas;
    Transform canvasRank;
    Transform HPtr;
    Transform gauge;
    GameObject rankPanel;
    Image scoreImg;
    Image HPImg;
    Image HPFrameImg;
    public Image gaugeImg;
    Button replay;
    Button exitBtn;
    Text score_txt;
    Text speedUp_txt;
    Text rankTxt;

    readonly string speedUp = "SPEED UP!";
    int lastScore = -1;
    public int coin = 0;

    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        scoreImg = canvas.GetChild(0).GetComponent<Image>();
        score_txt = scoreImg.transform.GetChild(0).GetComponent<Text>();
        speedUp_txt = canvas.GetChild(1).GetComponent<Text>();
        exitBtn = canvas.GetChild(2).GetComponent<Button>();
        replay = canvas.GetChild(3).GetChild(0).GetComponent<Button>();
        HPtr = canvas.GetChild(4);
        HPImg = HPtr.GetChild(0).GetComponent<Image>();
        HPFrameImg = HPtr.GetChild(1).GetComponent<Image>();
        gauge = canvas.GetChild(5);
        gaugeImg = gauge.GetChild(1).GetComponent<Image>();

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

    void CheckScore()
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

    public void OnOffRank(bool active)
    {
        scoreImg.gameObject.SetActive(!active);
        rankPanel.SetActive(active);
    }

    public void SetRank()
    {
        List<int> topScores = GameManager.Score.GetTopScores();
        string rankText = "";
        for (int i = 0; i < topScores.Count; i++)
            rankText += $"{(i + 1).ToString().PadLeft(2, ' ')}. {topScores[i],4}M\n";
        rankTxt.text = rankText;
    }

    public void GaugeUP(int addCoin)
    {
        coin += addCoin;
        gaugeImg.fillAmount = coin / 30f * 0.125f;

        if (gaugeImg.fillAmount >= 1)
            StartCoroutine(GameManager.instance.InvincibleCtrl());
    }

    public void OnOffGauge(bool active)
    {
        gauge.gameObject.SetActive(active);
    }

    public void UpdateHP(int hp)
        => HPImg.fillAmount = hp * 0.1f;

    public void OnOffHP(bool active)
    {
        HPImg.enabled = active;
        HPFrameImg.enabled = active;
    }

    void ReplayGame()
        => GameManager.instance.Reset();

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}