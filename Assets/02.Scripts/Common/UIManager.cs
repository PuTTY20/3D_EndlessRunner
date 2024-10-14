using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Transform tr;
    Text score_txt;
    Text speedUp_txt;
    int lastScore = -1;

    void Start()
    {
        tr = transform;

        Transform canvas = GameObject.Find("Canvas").transform;
        score_txt = canvas.GetChild(0).GetChild(0).GetComponent<Text>();
        speedUp_txt = canvas.GetChild(1).GetComponent<Text>();

        speedUp_txt.gameObject.SetActive(false);
    }

    void Update()
    {
        score_txt.text = $"{GameManager.instance.score}M";

        if ((GameManager.instance.score == 100 || GameManager.instance.score == 200) && GameManager.instance.score != lastScore)
        {
            lastScore = GameManager.instance.score; //프레임동안 값이 같아져버림 그래서 1번밖에 안함
            speedUp_txt.gameObject.SetActive(true);
            StartCoroutine(SpeedUpTextEffect("SPEED UP!"));
        }
    }

    IEnumerator SpeedUpTextEffect(string text)
    {
        speedUp_txt.text = string.Empty;

        foreach (char c in text)
        {
            speedUp_txt.text += c;
            yield return new WaitForSeconds(0.05f); // 타이핑 속도 조절
        }
        yield return new WaitForSeconds(0.8f); // 1초 대기
        speedUp_txt.gameObject.SetActive(false);
    }
}